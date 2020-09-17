using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientControl : MonoBehaviour
{
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
        print("Unhappy");
        VSFX.instance.CreateParticleSystem(VSFX.instance.unhappyPS, this.transform.position+Vector3.up, false);
        NewOrder();

    }

    public void Happy()
    {
        print("Happy");
        VSFX.instance.CreateParticleSystem(VSFX.instance.happyPS, this.transform.position, false);
        NewOrder();

    }
}
