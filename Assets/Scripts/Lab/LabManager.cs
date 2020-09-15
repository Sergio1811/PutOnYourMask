using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    public static LabManager instance;

    public GameObject substanceOne;
    public GameObject substanceTwo;    

    public enum ObjectType { Beer, Wheat, Rabbit, Dead, Egg, Box, Dinner };

    public CentrifugatorControl centifugator;
    public WarmerControl warmer;


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
                    AddToInventory(substanceOne);
                    break;

                case "LabSub2":
                    AddToInventory(substanceTwo);
                    break;

                case "Warmer":
                    GameObject Item3 = Instantiate(substanceOne);
                    warmer.AddObject(Item3);
                    break;

                case "Centrifugator":
                    GameObject Item2 = Instantiate(substanceOne);
                    centifugator.AddObject(Item2);
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

    public void AddToInventory(GameObject objectToAdd)
    {
        GameObject go = InventoryLab.instance.CheckInventorySpace();
        if (go != null)
        {
            GameObject Item = Instantiate(objectToAdd, go.transform);
            Item.transform.localPosition = Vector3.zero;
        }
    }

    public void WarmerControl()
    {

    }

    public void CentrifugatorControl()
    {

    }

}
