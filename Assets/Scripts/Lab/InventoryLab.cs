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

    public GameObject CheckInventorySpace(out int num)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;
                num = i;
                return slots[i].gameObject;
            }
        }

        num = 0;
        return null;
    }

    public void DeletePosition(int i)
    {
        isFull[i] = false;
    }
}
