using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmerControl : MonoBehaviour
{
    public GameObject inWarmer;

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
            timeUI.value = currentTimeWarming/ timeToWarmer;
        }
        else
        {
            isWarming= MachineFull();

        }
    }

    public void FinishWarming()
    {
        currentTimeWarming = 0;
        isWarming = false;
        inWarmer = null;
        timeUI.value = 0;
        timeUI.gameObject.SetActive(false);

        PopUpObject();

    }

    public bool MachineFull()
    {
        if (inWarmer != null)
        {
            timeUI.gameObject.SetActive(true);
            return true;
        }

        else return false;
    }

    public void AddObject(GameObject objectToWarm)
    {
        if (inWarmer == null)
            inWarmer = objectToWarm;
    }

    public void PopUpObject()
    {
        collectButton.gameObject.SetActive(true);

        collectButton.onClick.AddListener(
           delegate
           {
               collectButton.gameObject.SetActive(false);
           });

        collectButton.onClick.AddListener(
            delegate {
                LabManager.instance.AddToInventory(LabManager.instance.substanceOne);
            });

        collectButton.onClick.AddListener(
           delegate
           {
               collectButton.onClick.RemoveAllListeners();
           });

        itemCollectable.sprite = item;
    }
}
