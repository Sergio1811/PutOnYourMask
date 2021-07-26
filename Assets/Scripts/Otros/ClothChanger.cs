using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public GameObject headGO;
    public GameObject maskGO;
    public GameObject shirtGO;
    public GameObject pantsGO;
    public GameObject shoeGO;

    public Transform parent;
    public Transform parentMask;

    void Start()
    {
        Instantiate(GameManager.instance.headGO, parent.transform);
        Instantiate(GameManager.instance.maskGO, parentMask.transform);
        Instantiate(GameManager.instance.shirtGO, parent.transform);
        Instantiate(GameManager.instance.pantsGO, parent.transform);
        Instantiate(GameManager.instance.shoeGO, parent.transform);

        Destroy(headGO);
        Destroy(maskGO);
        Destroy(shirtGO);
        Destroy(pantsGO);
        Destroy(shoeGO);
    }

   
}
