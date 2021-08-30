using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int[] ropa;

    public Data (ClothManager controller)
    {
        ropa[0] = controller.AllCloth[0];
    }
}
