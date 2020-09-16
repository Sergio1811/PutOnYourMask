using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour
{
    public static LabManager instance;

    public GameObject substanceOne;
    public GameObject substanceTwo;    

    public enum ObjectType { Beer, Wheat, Rabbit, Dead, Egg, Box, Dinner };

    public CentrifugatorControl centifugator;
    public WarmerControl warmer;

    public RecipeDatabase recipeDB;
    public ItemDatabase itemDB;

    public GameObject itemTemplate;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }
    
    void Update()
    {
        ClickController();   
    }

    public void ClickController()
    {
        GameObject goClicked = InputManager.Instance.WhatAmIClicking();
        if(goClicked!=null)
        {
            switch (goClicked.tag)
            {
                case "Bin":
                    //Nothing?
                    break;

                case "LabSub1":
                    AddToInventory(itemDB.GetItem(1));
                    break;

                case "LabSub2":
                    AddToInventory(itemDB.GetItem(2));
                    break;

                case "Warmer":
                   // GameObject Item3 = Instantiate(itemTemplate);
                    warmer.AddObject(itemDB.GetItem("Rabbit"));
                    break;

                case "Centrifugator":
                    //Instantiate(itemDB.GetItem(3).gameObject);
                    centifugator.AddObject(itemDB.GetItem(1));
                    break;

                case "Character":
                    //Nothing
                    break;

                case "ObjectInventory":
                    //
                    break;

                default:
                    break;
            }
        }
    }

    public void AddToInventory(Item objectToAdd)
    {
        GameObject go = InventoryLab.instance.CheckInventorySpace();
        if (go != null)
        {
            GameObject Item = Instantiate(itemTemplate, go.transform);
            Item.transform.localPosition = Vector3.zero;
            Item.GetComponent<Image>().sprite = objectToAdd.icon;
        }
    }

    public void WarmerControl()
    {

    }

    public void CentrifugatorControl()
    {

    }

}
