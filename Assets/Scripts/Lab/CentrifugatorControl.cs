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
    public GameObject liquido;
    public Rotation batidora;

    bool isCentrifugating = false;
    [HideInInspector]
    public bool objectInMachine;

    public Animator m_Animator;

    void Start()
    {
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
        batidora.rotSpeed = 0;
        liquido.SetActive(false);
        liquido.GetComponent<Rotation>().rotSpeed = 0;
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
            m_Animator.SetBool("MachineFull", MachineFull());

        }
    }

    public void FinishCentrifugate()
    {
        PopUpObject();

        objectInMachine = true;
        batidora.rotSpeed = 0;
        liquido.GetComponent<Rotation>().rotSpeed = 0;
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

        VSFX.instance.PlayAudio(VSFX.instance.centrifugatorUsageSound);
        timeUI.gameObject.SetActive(true);
        batidora.rotSpeed = 120;
        liquido.SetActive(true);
        liquido.GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
        liquido.GetComponent<Rotation>().rotSpeed = 240;
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
        if (!objectInMachine)
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
    }

    public void PopUpObject()
    {
        int itemIDFromRecipe = LabManager.instance.recipeDB.GetItemFromRecipe(inCentrifugator);
        collectButton.gameObject.SetActive(true);

        if (itemIDFromRecipe == 0)
        {
            //PONER EXPLOSION DE LA MAQUINA O REACCION DEL PLAYER
        }

        Item itemToCollect = LabManager.instance.itemDB.GetItem(itemIDFromRecipe);
        itemCollectable.sprite = itemToCollect.icon;

        //poner cambio color del liquidillo

        collectButton.onClick.AddListener(
          delegate
          {
              collectButton.gameObject.SetActive(false);
              liquido.SetActive(false);
              objectInMachine = false;
          });

        collectButton.onClick.AddListener(
            delegate
            {
                if (LabManager.instance.AddToInventory(itemToCollect))
                    VSFX.instance.PlayAudio(VSFX.instance.bottleSounds[Random.Range(0, VSFX.instance.bottleSounds.Length)]);
            });

        collectButton.onClick.AddListener(
           delegate
           {
               collectButton.onClick.RemoveAllListeners();
           });

    }

}

