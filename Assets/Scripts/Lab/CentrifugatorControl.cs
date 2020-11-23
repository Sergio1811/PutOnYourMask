using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentrifugatorControl : MonoBehaviour
{
    //change for only ids
    public int[] inCentrifugator = new int[2];

    public Image timeUI;
    public Button collectButton;
    public Image itemCollectable;

    public float timeToCentrifugate;
    float currentTimeCentrifugating;

    bool isCentrifugating = false;

    void Start()
    {
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentTimeCentrifugating >= timeToCentrifugate)
        {
            FinishCentrifugate();
        }

        if (isCentrifugating)
        {
            currentTimeCentrifugating += Time.deltaTime;
            timeUI.fillAmount = currentTimeCentrifugating / timeToCentrifugate;
        }
        else
        {
            isCentrifugating = MachineFull();

        }
    }

    public void FinishCentrifugate()
    {
        PopUpObject();

        currentTimeCentrifugating = 0;
        isCentrifugating = false;
        EmptyMachine();
        timeUI.fillAmount = 0;
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(true);

    }

    public bool MachineFull()
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            if (inCentrifugator[i] == 0)
            {
                return false;
            }
        }

        timeUI.gameObject.SetActive(true);
        return true;
    }

    public void EmptyMachine()
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            inCentrifugator[i] = 0;
        }
    }

    public void AddObject(Item objectToCentrifugate)
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            if (inCentrifugator[i] == 0)
            {
                inCentrifugator[i] = objectToCentrifugate.id;
                break;
            }
        }
    }

    public void PopUpObject()
    {
        int itemIDFromRecipe = LabManager.instance.recipeDB.GetItemFromRecipe(inCentrifugator);

        collectButton.gameObject.SetActive(true);
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
