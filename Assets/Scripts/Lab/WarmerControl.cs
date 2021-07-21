using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmerControl : MonoBehaviour
{
    public int[] inWarmer = new int[1];

    public Image timeUI;
    public Button collectButton;
    public Image itemCollectable;

    public float timeToWarmer;
    float currentTimeWarming;
    public GameObject liquido;


    bool isWarming = false;

    [HideInInspector]
    public bool objectInMachine;

    public Animator m_Animator;
    public Transform m_ExplosionPos;
    void Start()
    {
        if (VSFX.instance.SmokeColumnPS != null && VSFX.instance.FireBunPS != null)
        {
            VSFX.instance.SmokeColumnPS.GetComponent<ParticleSystem>().Stop();
            VSFX.instance.FireBunPS.GetComponent<ParticleSystem>().Stop();
        }
        collectButton.gameObject.SetActive(false);
        liquido.SetActive(false);
        liquido.GetComponent<Rotation>().rotSpeed = 0;
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
            timeUI.fillAmount = currentTimeWarming / timeToWarmer;
        }
        else
        {
            isWarming = MachineFull();
            m_Animator.SetBool("MachineFull", MachineFull());

        }
    }

    public void FinishWarming()
    {
        PopUpObject();

        objectInMachine = true;
        liquido.GetComponent<Rotation>().rotSpeed = 0;

        if (VSFX.instance.SmokeColumnPS != null && VSFX.instance.FireBunPS != null)
        {
            VSFX.instance.SmokeColumnPS.GetComponent<ParticleSystem>().Stop();
            VSFX.instance.FireBunPS.GetComponent<ParticleSystem>().Stop();
        }

        VSFX.instance.CreateParticleSystem(VSFX.instance.SmokePuffPS, this.transform.position, false);
        currentTimeWarming = 0;
        isWarming = false;
        inWarmer[0] = 0;
        timeUI.fillAmount = 0;
        timeUI.gameObject.SetActive(false);
    }

    public bool MachineFull()
    {
        if (inWarmer[0] != 0)
        {
            if (VSFX.instance.SmokeColumnPS != null && VSFX.instance.FireBunPS != null)
            {
                VSFX.instance.SmokeColumnPS.GetComponent<ParticleSystem>().Play();
                VSFX.instance.FireBunPS.GetComponent<ParticleSystem>().Play();
                VSFX.instance.PlayAudio(VSFX.instance.warmerUsageSound);

            }
            liquido.SetActive(true);
            
            liquido.GetComponent<Rotation>().rotSpeed = 240;
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
        VSFX.instance.PlayAudio(VSFX.instance.popUpSound);

        if (itemIDFromRecipe == 0)
        {
            LabManager.instance.player.animator.SetTrigger("Susto");
            VSFX.instance.CreateParticleSystem(VSFX.instance.explosionMachinePS, m_ExplosionPos.position, false);
            //PONER EXPLOSION DE LA MAQUINA O REACCION DEL PLAYER
        }
        else
        {
            VSFX.instance.CreateParticleSystem(VSFX.instance.finishedPS, m_ExplosionPos.position, false);
        }


        Item itemToCollect = LabManager.instance.itemDB.GetItem(itemIDFromRecipe);
        itemCollectable.sprite = itemToCollect.icon;
        Material mymat = liquido.GetComponent<MeshRenderer>().material;
        mymat.SetColor("_EmissionColor", itemToCollect.color);
        mymat.SetColor("_Color", itemToCollect.color);

        print(itemToCollect.color);
        collectButton.onClick.AddListener(
           delegate
           {
               objectInMachine = false;
               collectButton.gameObject.SetActive(false);
               mymat.SetColor("_EmissionColor", Color.white);

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
