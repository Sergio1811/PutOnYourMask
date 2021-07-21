using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CatalogueController : MonoBehaviour
{
    public GameObject[] postIts;
    public GameObject[] collections;
    public Transform parent;
    public Dictionary<MenuController.Clothing, int> cloth;

    private void Start()
    {
        cloth = new Dictionary<MenuController.Clothing, int>() {
        {MenuController.Clothing.Head, 0},
        {MenuController.Clothing.Mask, 1},
        {MenuController.Clothing.Shirt, 2},
        {MenuController.Clothing.Pants, 3},
        {MenuController.Clothing.Shoes, 4},
        {MenuController.Clothing.Special, 5}
        };
    }
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

        postItClicked.transform.SetSiblingIndex(postIts.Length+1);
        postItClicked.GetComponent<SectionActivator>().section.SetActive(true);
    }

    public void PostItClickedObject(MenuController.Clothing clothing)
    {
        foreach (var item in postIts)
        {
            item.transform.SetAsFirstSibling();
        }
        foreach (var item in collections)
        {
            item.SetActive(false);
        }

        postIts[cloth[clothing]].transform.SetSiblingIndex(postIts.Length+1);
        postIts[cloth[clothing]].GetComponent<SectionActivator>().section.SetActive(true);
    }
    public void PostItClickedObject(int clothing)
    {
        foreach (var item in postIts)
        {
            item.transform.SetAsFirstSibling();
            item.SetActive(false);
        }
        foreach (var item in collections)
        {
            item.SetActive(false);
        }

        if (clothing == 2)
        {
            postIts[clothing].transform.SetSiblingIndex(postIts.Length+1);
            postIts[clothing].GetComponent<SectionActivator>().section.SetActive(true);
            postIts[clothing].SetActive(true);

            postIts[clothing + 1].SetActive(true);

            postIts[clothing + 2].SetActive(true);

        }
        else
        {
            postIts[clothing].transform.SetSiblingIndex(postIts.Length+1);
            postIts[clothing].GetComponent<SectionActivator>().section.SetActive(true);
            postIts[clothing].SetActive(true);

        }
    }
    public void Back()
    {
        foreach (var item in postIts)
        {
            item.SetActive(true);
        }
    }
}
