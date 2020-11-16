using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaskDropHandler : MonoBehaviour
{

    public void Update ()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(AccessControlManager.instance.myCanvas.transform as RectTransform, Input.mousePosition, AccessControlManager.instance.myCanvas.worldCamera, out pos);
        transform.position = AccessControlManager.instance.myCanvas.transform.TransformPoint(pos);

        if (Input.touchCount==0 && Input.GetMouseButtonUp(0))
        {
            RectTransform invPanel = transform as RectTransform;

            if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("CharsHead"))
                    {
                        hit.collider.gameObject.GetComponent<CharsPPMovement>().mask.SetActive(true);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

}
