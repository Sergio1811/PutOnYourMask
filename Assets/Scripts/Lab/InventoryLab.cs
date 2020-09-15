using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLab : MonoBehaviour
{
    public static InventoryLab instance;
    public bool[] isFull;
    public GameObject[] slots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public GameObject CheckInventorySpace()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;                
                return slots[i].gameObject;
            }
        }

        return null;
    }
}
