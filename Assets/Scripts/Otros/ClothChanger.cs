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
    public GameObject cabezaGO;
    

    public Transform parent;
    public Transform parentMask;

    public RuntimeAnimatorController animator;
    public PlayerLabMovement player;

    public Vector3 tempPosM;
    void Start()
    {      
        player.animator.Add(Instantiate(GameManager.instance.headGO, parent.transform).GetComponent<Animator>());
        player.animator.Add(Instantiate(GameManager.instance.shirtGO, parent.transform).GetComponent<Animator>());
        player.animator.Add(Instantiate(GameManager.instance.pantsGO, parent.transform).GetComponent<Animator>());
        player.animator.Add(Instantiate(GameManager.instance.shoeGO, parent.transform).GetComponent<Animator>());
        GameObject cabesa = Instantiate(cabezaGO, parent.transform);
        player.animator.Add(cabesa.GetComponent<Animator>());

        foreach (var item in player.animator)
        {
            item.runtimeAnimatorController = animator;
        }

        Destroy(headGO);
        Destroy(shirtGO);
        Destroy(pantsGO);
        Destroy(shoeGO);
       // Destroy(cabezaGO);

      
        Destroy(maskGO);
        parentMask = cabesa.GetComponent<Cabeza>().cabeza;

        maskGO = Instantiate(GameManager.instance.maskGO, parentMask.transform);
        maskGO.transform.localPosition = tempPosM;
    }

   
}
