using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabManager : MonoBehaviour
{
    public static LabManager instance;

    public GameObject substanceOne;
    public GameObject substanceTwo;
    public PlayerLabMovement player;

    public enum ObjectType { Beer, Wheat, Rabbit, Dead, Egg, Box, Dinner };

    public CentrifugatorControl centrifugator;
    public WarmerControl warmer;

    public RecipeDatabase recipeDB;
    public ItemDatabase itemDB;

    public GameObject itemTemplate;

    #region Waypoints
    [Header("Waypoints")]
    public Transform mostradorPosition;
    public Transform sub1;
    public Transform sub2;
    public Transform sub3;
    public Transform trash;
    public Transform warmerPos;
    public Transform centrifugatorPos;

    #endregion

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
            var pos = Vector3.zero;
            if (Input.touchCount > 0)
            {
                 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            switch (goClicked.tag)
            {
                case "Bin":
                    //Nothing?
                    player.nextPoint = trash.gameObject.transform.position;
                    break;

                case "LabSub1":
                    player.nextPoint = sub1.gameObject.transform.position;
                    AddToInventory(itemDB.GetItem(1));
                    break;

                case "LabSub2":
                    player.nextPoint = sub2.gameObject.transform.position;
                    AddToInventory(itemDB.GetItem(2));
                    break;

                case "LabSub3":
                    player.nextPoint = sub3.gameObject.transform.position;
                    AddToInventory(itemDB.GetItem(3));
                    break;

                case "Warmer":
                    player.nextPoint = warmer.gameObject.transform.position;
                    // GameObject Item3 = Instantiate(itemTemplate);
                    break;

                case "Centrifugator":
                    player.nextPoint = centrifugatorPos.transform.position;
                    //Instantiate(itemDB.GetItem(3).gameObject);
                    break;

                case "Character":
                    player.nextPoint = mostradorPosition.transform.position;
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
        int number;
        GameObject go = InventoryLab.instance.CheckInventorySpace(out number);
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
