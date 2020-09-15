using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public LabManager.ObjectType thisType;

    private void Update()
    {
        if(InputManager.Instance.WhatAmIClicking()==this.gameObject)
        InputManager.Instance.DragAndDrop(this.gameObject, true);
    }
}
