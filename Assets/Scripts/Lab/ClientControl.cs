using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientControl : MonoBehaviour
{
    public Animator animator;
    public Slider timeUI;
    public GameObject canvasClient;

    public float timeToWait;
    float currentTimeToZero;

    public Image imageItemWanted;
    public Item wantedItem;

    public Transform[] puntosDeEspera;
    public Transform[] puntosDeHuida;

    int point;

    bool waiting = false;

    public float speed;

    private void Start()
    {
        point = Random.Range(0, 1);
    }

    void Update()
    {
        if (!waiting)
        {
            if (Vector3.Distance(this.transform.position, puntosDeEspera[point].transform.position) < 0.1f)
            {
                canvasClient.SetActive(true);
                waiting = true;
                animator.SetBool("Walking", false);
                NewOrder();

            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, puntosDeEspera[point].transform.position, speed * Time.deltaTime);
        }
        else
        {
            timeUI.value = currentTimeToZero / timeToWait;
            currentTimeToZero -= Time.deltaTime;

            if (currentTimeToZero <= 0)
            {
                Unhappy();
            }
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
