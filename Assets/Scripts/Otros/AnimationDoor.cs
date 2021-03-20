using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoor : MonoBehaviour
{
    public GameObject rightDoor;
    public GameObject leftDoor;

    public float leftDoorTarget;
    public float rightDoorTarget;

    Vector3 startPosLeftDoor;
    Vector3 startPosRightDoor;
    Vector3 targetPosLeftDoor;
    Vector3 targetPosRightDoor;

    public float leftDoorScale;
    public float rightDoorScale;

    public enum doorState { OPENING, CLOSING, WAITING, NONE };
    public doorState currentDoorState;

    public float speedOpening;
    public float speedClosing;

    public float WaitTime;

    private void Start()
    {
        currentDoorState = doorState.WAITING;
        startPosLeftDoor = leftDoor.transform.position;
        startPosRightDoor = rightDoor.transform.position;
        targetPosLeftDoor = new Vector3(leftDoorTarget, leftDoor.transform.position.y, leftDoor.transform.position.z);
        targetPosRightDoor = new Vector3(rightDoorTarget, rightDoor.transform.position.y, rightDoor.transform.position.z);
    }

    void Update()
    {
        switch (currentDoorState)
        {
            case doorState.OPENING:
                rightDoor.transform.position = Vector3.LerpUnclamped(rightDoor.transform.position, targetPosRightDoor, speedOpening / 2 * Time.deltaTime);
                leftDoor.transform.position = Vector3.LerpUnclamped(leftDoor.transform.position, targetPosLeftDoor, speedOpening / 2 * Time.deltaTime);
                rightDoor.transform.localScale = Vector3.LerpUnclamped(rightDoor.transform.localScale, new Vector3(rightDoorScale, 1, 1), speedOpening * Time.deltaTime);
                leftDoor.transform.localScale = Vector3.LerpUnclamped(leftDoor.transform.localScale, new Vector3(leftDoorScale, 1, 1), speedOpening * Time.deltaTime);

                if (rightDoor.transform.localScale.x<=rightDoorScale)
                {
                    StartCoroutine(timeOpen());
                }

                break;
            case doorState.CLOSING:
                rightDoor.transform.position = Vector3.LerpUnclamped(rightDoor.transform.position, startPosRightDoor, speedClosing / 3 * Time.deltaTime);
                leftDoor.transform.position = Vector3.LerpUnclamped(leftDoor.transform.position, startPosLeftDoor, speedClosing / 3 * Time.deltaTime);
                rightDoor.transform.localScale = Vector3.LerpUnclamped(rightDoor.transform.localScale, new Vector3(1, 1, 1), speedClosing * Time.deltaTime);
                leftDoor.transform.localScale = Vector3.LerpUnclamped(leftDoor.transform.localScale, new Vector3(1, 1, 1), speedClosing * Time.deltaTime);
                break;

            case doorState.WAITING:
                break;

            default:
                break;
        }


    }

    IEnumerator timeOpen()
    {
        yield return new WaitForSeconds(WaitTime);
        currentDoorState = doorState.WAITING;
        StartCoroutine(timeToClose());
    }
    IEnumerator timeToClose()
    {
        yield return new WaitForSeconds(WaitTime);
        currentDoorState = doorState.CLOSING;
    }
}
