using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatalogueController : MonoBehaviour
{
    public GameObject[] postIts;
    public GameObject[] collections;
    public Transform parent;

    public void PostItClicked(GameObject postItClicked)
    {
        foreach (var item in postIts)
        {
            item.transform.SetAsFirstSibling();
        }
        foreach (var item in collections)
        {
            item.SetActive(false);
        }

        postItClicked.transform.SetSiblingIndex(postIts.Length);
        postItClicked.GetComponent<SectionActivator>().section.SetActive(true);
    }
}
