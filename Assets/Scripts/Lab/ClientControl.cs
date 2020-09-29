using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientControl : MonoBehaviour
{
    public Animator animator;
    public Slider timeUI;

    public float timeToWait;
    float currentTimeToZero;

    public Image imageItemWanted;
    public Item wantedItem;

    private void Start()
    {
        NewOrder();
    }

    void Update()
    {
       /* if(speed>0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }*/

        timeUI.value = currentTimeToZero / timeToWait;
        currentTimeToZero -= Time.deltaTime;

        if(currentTimeToZero<=0)
        {
            Unhappy();
        }
    }

    public void NewOrder()
    {
        currentTimeToZero = timeToWait;
        wantedItem = LabManager.instance.itemDB.GetRandomItem();
        imageItemWanted.sprite = wantedItem.icon;
    }

    public void ItemDropped(Item item)
    {
        if(item.id==wantedItem.id)
        {
            Happy();
        }
        else
        {
            Unhappy();
        }
    }

    public void Unhappy()
    {
        VSFX.instance.CreateParticleSystem(VSFX.instance.unhappyPS, this.transform.position+Vector3.up, false);
        NewOrder();
    }

    public void Happy()
    {
        VSFX.instance.CreateParticleSystem(VSFX.instance.happyPS, this.transform.position+Vector3.zero, false);
        NewOrder();
    }
}
