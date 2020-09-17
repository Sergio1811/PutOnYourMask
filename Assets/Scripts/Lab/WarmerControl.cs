using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmerControl : MonoBehaviour
{
    public int[] inWarmer = new int[1];

    public Slider timeUI;
    public Button collectButton;
    public Image itemCollectable;

    public Sprite item;

    public float timeToWarmer;
    float currentTimeWarming;

    bool isWarming = false;

    void Start()
    {
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentTimeWarming >= timeToWarmer)
        {
            FinishWarming();
        }

        if (isWarming)
        {
            currentTimeWarming += Time.deltaTime;
            timeUI.value = currentTimeWarming / timeToWarmer;
        }
        else
        {
            isWarming = MachineFull();

        }
    }

    public void FinishWarming()
    {
        PopUpObject();

        currentTimeWarming = 0;
        isWarming = false;
        inWarmer[0] = 0;
        timeUI.value = 0;
        timeUI.gameObject.SetActive(false);
    }

    public bool MachineFull()
    {
        if (inWarmer[0] != 0)
        {
            timeUI.gameObject.SetActive(true);
            return true;
        }

        else
        {
            return false;
        }
    }

    public void AddObject(Item objectToWarm)
    {
        if (inWarmer[0] == 0)
        {
            inWarmer[0] = objectToWarm.id;
        }
    }

    public void PopUpObject()
    {
        collectButton.gameObject.SetActive(true);

        int itemIDFromRecipe = LabManager.instance.recipeDB.GetItemFromRecipe(inWarmer);

        if (itemIDFromRecipe == 0)
        {
            itemCollectable.sprite = Resources.Load<Sprite>("Sprites/Lab/RedCross");

            collectButton.onClick.AddListener(
                delegate
                {
                    collectButton.gameObject.SetActive(false);
                });

            collectButton.onClick.AddListener(
               delegate
               {
                   collectButton.onClick.RemoveAllListeners();
               });
        }

        else
        {

            Item itemToCollect = LabManager.instance.itemDB.GetItem(itemIDFromRecipe);
            itemCollectable.sprite = itemToCollect.icon;

            collectButton.onClick.AddListener(
               delegate
               {
                   collectButton.gameObject.SetActive(false);
               });

            collectButton.onClick.AddListener(
                delegate
                {
                    LabManager.instance.AddToInventory(itemToCollect);
                });

            collectButton.onClick.AddListener(
               delegate
               {
                   collectButton.onClick.RemoveAllListeners();
               });

        }
    }
}
