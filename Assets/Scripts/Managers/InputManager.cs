using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public Camera Camera;
    private float targetZoom;
    [SerializeField] private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;
    [SerializeField] private float minCameraY;
    [SerializeField] private float maxCameraY;
    [SerializeField] private float minCameraX;
    [SerializeField] private float maxCameraX;
    [SerializeField] private float minCameraZ;
    [SerializeField] private float maxCameraZ;

    #region DragAndDropParameters
    Vector3 dragAndDropScreenSpace;
    Vector3 dragAndDropOffset;
    bool dragAndDropAllowed = false;
    Vector3 dragOriginalPosition;
    #endregion

    #region PinchParameters
    public float minZoom = 8;
    public float maxZoom = 16;
    float speedPinchControl = 0.01f;
    Vector3 originalPos;
    Vector3 pointToZoom = Vector3.zero;
    protected Plane Plane;
    float currentZoom;
    #endregion

    #region SwipeParameters
    public System.Action<SwipeAction> onSwipe;
    public System.Action<SwipeAction> onLongPress;

    [Range(0f, 200f)]
    public float minSwipeLength = 100f;

    public float longPressDuration = 0.5f;

    Vector2 currentSwipe;
    SwipeAction currentSwipeAction = new SwipeAction();
    #endregion

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("InputManager");
                    instance = container.AddComponent<InputManager>();
                }
            }

            return instance;
        }
    }


    void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }

        targetZoom = Camera.orthographicSize;
        originalPos = Camera.transform.position;

        /*
        if (Camera.orthographic)
        {
            Camera.orthographicSize = maxZoom;
        }
        else
        {
            Camera.fieldOfView = maxZoom;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        DetectSwipe();

        //Update Plane
        if (Input.touchCount >= 1)
        {
            Plane.SetNormalAndPosition(transform.up, transform.position);
        }

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
            {
                return;
            }

            currentZoom += zoom;

            if (currentZoom < 0 && zoom < 1)
            {
                zoom = 1;
                currentZoom = 0;
            }
            else if (currentZoom > minZoom && zoom < 1)
            {
                zoom = 1;
                currentZoom = maxZoom;
            }
            //Move cam amount the mid ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);


        }

#if UNITY_STANDALONE || UNITY_EDITOR
        /*Zoom();*/
#endif

#if UNITY_ANDROID
        // PinchZoom();
#endif
    }

    public GameObject DragAndDrop(GameObject objectToDrag, bool returnToPos)
    {

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButton(0)) //If clicked
        {

            if (Input.GetMouseButtonDown(0)) //OnMouseDown take parameters and allow movement
            {
                if (WhatAmIClicking() == objectToDrag) //Object to drag is correct
                {
                    dragOriginalPosition = objectToDrag.transform.position;
                    dragAndDropScreenSpace = Camera.WorldToScreenPoint(objectToDrag.transform.position); //take screenpos of the object
                    dragAndDropOffset = objectToDrag.transform.position - Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragAndDropScreenSpace.z)); //Calculate offset to have a good visual perception of the drag
                    dragAndDropAllowed = true;
                }
            }

            if (dragAndDropAllowed) //On Mouse Moving and clicked
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragAndDropScreenSpace.z); //position in screen of the object
                Vector3 currentPosition = Camera.ScreenToWorldPoint(currentScreenSpace) + dragAndDropOffset; //calculate position in world with the offset
                objectToDrag.transform.position = currentPosition;
            }

        }
        if (Input.GetMouseButtonUp(0) && dragAndDropAllowed) //mouse Up movement not allowed
        {
            RaycastHit hit;
            if (Physics.Raycast(Input.mousePosition, Vector3.forward, out hit))
            {
                return hit.collider.gameObject;
            }
            dragAndDropAllowed = false;

            if (returnToPos)
            {
                objectToDrag.transform.position = dragOriginalPosition;
            }
        }
#endif

#if UNITY_ANDROID //same as above but in mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (WhatAmIClicking() == objectToDrag)
                    {
                        dragOriginalPosition = objectToDrag.transform.position;

                        dragAndDropScreenSpace = Camera.WorldToScreenPoint(objectToDrag.transform.position);
                        dragAndDropOffset = objectToDrag.transform.position - Camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, dragAndDropScreenSpace.z));

                        dragAndDropAllowed = true;
                    }

                    break;

                case TouchPhase.Moved:
                    if (dragAndDropAllowed)
                    {
                        Vector3 currentScreenSpace = new Vector3(touch.position.x, touch.position.y, dragAndDropScreenSpace.z);
                        Vector3 currentPosition = Camera.ScreenToWorldPoint(currentScreenSpace) + dragAndDropOffset;
                        objectToDrag.transform.position = currentPosition;
                    }
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    RaycastHit hit;
                    if (Physics.Raycast(Input.mousePosition, Vector3.forward, out hit))
                    {
                        return hit.collider.gameObject;
                    }

                    dragAndDropAllowed = false;
                    if (returnToPos)
                    {
                        objectToDrag.transform.position = dragOriginalPosition;
                    }

                    break;

                case TouchPhase.Canceled:
                    break;

                default:
                    break;
            }
        }

        return null;
#endif
    }

    public GameObject WhatAmIClicking()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))//Cambiar si es necesario
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        return hit.collider.gameObject;
                    }

                }
            }
        }
#endif
        return null;
    }

    public GameObject WhatAmIClicking2D()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))//Cambiar si es necesario
        {
            Vector2 ray = Camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            Transform target = null;
            //If something was hit, the RaycastHit2D.collider will not be null and the the object must have the "Monkey" tag and target has to be null
            if (hit.collider != null && hit.collider.tag == "Monkey" && target == null)
            {
                target = hit.collider.transform; // Sets the target to be the transform that was hit
            }

            if (target != null)
            {
                // target.position = worldPoint; // Moves target with the mouse
            }


            if (Input.GetMouseButtonUp(0))
            {
                target = null; // Sets the target to null again
            }
            return hit.collider.gameObject;
        }


#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        return hit.collider.gameObject;
                    }

                }
            }
        }
#endif
        return null;
    }

    #region ZoomManager 

    public void Zoom()
    {
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0) //Zoom in
        {
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 desiredPosition;

            if (Physics.Raycast(ray, out hit))
            {
                desiredPosition = hit.point; //Position to zoom
            }
            else
            {
                desiredPosition = Camera.transform.position;
            }

            float distance = Vector3.Distance(desiredPosition, Camera.transform.position);
            Vector3 direction = Vector3.Normalize(desiredPosition - Camera.transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel")); //Where to zoom vector

            Camera.transform.position += direction;

            if (Vector3.Distance(Camera.transform.position, hit.point) < 6 || Camera.transform.position.y < 1) //Control the zoom
            {
                Camera.transform.position -= direction;
            }
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0) //zoom out
        {
            Vector3 desiredPosition = originalPos; //just zoom to original pos

            float distance = Vector3.Distance(desiredPosition, Camera.transform.position);
            Vector3 direction = Vector3.Normalize(desiredPosition - Camera.transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel"));

            Camera.transform.position -= direction;
        }*/

        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        if (scrollData > 0)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Camera.ScreenToWorldPoint(Input.mousePosition), zoomLerpSpeed * Time.deltaTime);
        }
        else if (scrollData < 0)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, originalPos, zoomLerpSpeed * Time.deltaTime);
        }

        if (scrollData != 0)
        {
            Camera.transform.position = new Vector3(Mathf.Clamp(Camera.transform.position.x, minCameraX, maxCameraX), Mathf.Clamp(Camera.transform.position.y, minCameraY, maxCameraY), Mathf.Clamp(Camera.transform.position.z, minCameraZ, maxCameraZ));
        }
    }

    public void PinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float pinchMultiplier = currentMagnitude / prevMagnitude;

            if (pointToZoom == Vector3.zero)
            {
                pointToZoom = touchZero.position - touchOne.position;
            }

            if (pinchMultiplier < 1)
            {/*
                RaycastHit hit;
                Ray ray = Camera.ScreenPointToRay(pointToZoom);
                Vector3 desiredPosition;

                if (Physics.Raycast(ray, out hit))
                {
                    desiredPosition = hit.point;
                }
                else
                {
                    desiredPosition = Camera.transform.position;
                }

                float distance = Vector3.Distance(desiredPosition, Camera.transform.position);
                Vector3 direction = Vector3.Normalize(desiredPosition - Camera.transform.position) * (distance * pinchMultiplier * speedPinchControl);

                Camera.transform.position += direction;
                if (Vector3.Distance(Camera.transform.position, hit.point) < 6 || Camera.transform.position.y < 1)
                {
                    Camera.transform.position -= direction;
                }*/

                targetZoom -= pinchMultiplier * zoomFactor;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

                Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Camera.ScreenToWorldPoint(pointToZoom), zoomLerpSpeed * Time.deltaTime);

                Camera.transform.position = new Vector3(Mathf.Clamp(Camera.transform.position.x, minCameraX, maxCameraX), Mathf.Clamp(Camera.transform.position.y, minCameraY, maxCameraY), Mathf.Clamp(Camera.transform.position.z, minCameraZ, maxCameraZ));

            }
            else
            {/*
                Vector3 desiredPosition = originalPos;

                float distance = Vector3.Distance(desiredPosition, Camera.transform.position);
                Vector3 direction = Vector3.Normalize(desiredPosition - Camera.transform.position) * (distance * pinchMultiplier);

                Camera.transform.position -= direction;*/
                targetZoom -= pinchMultiplier * zoomFactor;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
                Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, originalPos, zoomLerpSpeed * Time.deltaTime);
                Camera.transform.position = new Vector3(Mathf.Clamp(Camera.transform.position.x, minCameraX, maxCameraX), Mathf.Clamp(Camera.transform.position.y, minCameraY, maxCameraY), Mathf.Clamp(Camera.transform.position.z, minCameraZ, maxCameraZ));

            }
        }
        else
        {
            pointToZoom = Vector3.zero;
        }
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
        {
            return Vector3.zero;
        }

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
        {
            return rayNow.GetPoint(enterNow);
        }

        return Vector3.zero;
    }

    #endregion

    #region SwipeManager

    public void DetectSwipe()
    {

        if (Input.touches.Length > 0)
        {
            Touch t = Input.touches[0];

            if (t.phase == TouchPhase.Began)
            {
                ResetCurrentSwipeAction(t);
            }

            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                UpdateCurrentSwipeAction(t);
                if (!currentSwipeAction.longPress && currentSwipeAction.duration > longPressDuration && currentSwipeAction.longestDistance < minSwipeLength)
                {
                    currentSwipeAction.direction = SwipeDirection.None; // Invalidate current swipe action
                    currentSwipeAction.longPress = true;
                    if (onLongPress != null)
                    {
                        onLongPress(currentSwipeAction); // Fire event
                    }
                    return;
                }
            }

            if (t.phase == TouchPhase.Ended)
            {
                UpdateCurrentSwipeAction(t);

                // Make sure it was a legit swipe, not a tap, or long press
                if (currentSwipeAction.distance < minSwipeLength || currentSwipeAction.longPress) // Didnt swipe enough or this is a long press
                {
                    currentSwipeAction.direction = SwipeDirection.None; // Invalidate current swipe action
                    return;
                }

                if (onSwipe != null)
                {
                    onSwipe(currentSwipeAction); // Fire event
                }
            }
        }
    }

    void ResetCurrentSwipeAction(Touch t)
    {
        currentSwipeAction.duration = 0f;
        currentSwipeAction.distance = 0f;
        currentSwipeAction.longestDistance = 0f;
        currentSwipeAction.longPress = false;
        currentSwipeAction.startPosition = new Vector2(t.position.x, t.position.y);
        currentSwipeAction.startTime = Time.time;
        currentSwipeAction.endPosition = currentSwipeAction.startPosition;
        currentSwipeAction.endTime = currentSwipeAction.startTime;
    }

    void UpdateCurrentSwipeAction(Touch t)
    {
        currentSwipeAction.endPosition = new Vector2(t.position.x, t.position.y);
        currentSwipeAction.endTime = Time.time;
        currentSwipeAction.duration = currentSwipeAction.endTime - currentSwipeAction.startTime;
        currentSwipe = currentSwipeAction.endPosition - currentSwipeAction.startPosition;
        currentSwipeAction.rawDirection = currentSwipe;
        currentSwipeAction.direction = GetSwipeDirection(currentSwipe);
        currentSwipeAction.distance = Vector2.Distance(currentSwipeAction.startPosition, currentSwipeAction.endPosition);
        if (currentSwipeAction.distance > currentSwipeAction.longestDistance) // If new distance is longer than previously longest
        {
            currentSwipeAction.longestDistance = currentSwipeAction.distance; // Update longest distance
        }
    }

    SwipeDirection GetSwipeDirection(Vector2 direction)
    {
        var angle = Vector2.Angle(Vector2.up, direction.normalized); // Degrees
        var swipeDirection = SwipeDirection.None;

        if (direction.x > 0) // Right
        {
            if (angle < 22.5f) // 0.0 - 22.5
            {
                swipeDirection = SwipeDirection.Up;
            }

            else if (angle < 112.5f) // 67.5 - 112.5
            {
                swipeDirection = SwipeDirection.Right;
            }

            else if (angle < 180.0f) // 157.5 - 180.0
            {
                swipeDirection = SwipeDirection.Down;
            }
        }
        else // Left
        {
            if (angle < 22.5f) // 0.0 - 22.5
            {
                swipeDirection = SwipeDirection.Up;
            }

            else if (angle < 112.5f) // 67.5 - 112.5
            {
                swipeDirection = SwipeDirection.Left;
            }

            else if (angle < 180.0f) // 157.5 - 180.0
            {
                swipeDirection = SwipeDirection.Down;
            }
        }

        return swipeDirection;
    }

    #endregion
}

public struct SwipeAction
{
    public SwipeDirection direction;
    public Vector2 rawDirection;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float startTime;
    public float endTime;
    public float duration;
    public bool longPress;
    public float distance;
    public float longestDistance;

    public override string ToString()
    {
        return string.Format("[SwipeAction: {0}, From {1}, To {2}, Delta {3}, Time {4:0.00}s]", direction, rawDirection, startPosition, endPosition, duration);
    }
}

public enum SwipeDirection
{
    None, // Basically means an invalid swipe
    Up,
    Right,
    Down,
    Left,
}