using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientControl : MonoBehaviour
{
    LookGameObject dondeMiro;
    GameObject player;
    public Animator animator;
    public Image timeUI;
    public GameObject canvasClient;
    //public Animation doorOpening;
    public AnimationDoor doorOpening;

    public float timeToWait;
    float currentTimeToZero;

    public Image imageItemWanted;
    public Item wantedItem;

    public Transform puntosDeEspera;
    public Transform puntosDeHuida;
    public Transform puntoPuerta;
    public Transform puntoInicio;
    Vector3 NextObjetivo;

    public enum States { Wait, Huye, Puerta, Inicio, Mostrador};
    public States currentState = States.Wait;

    public float speed;

    private void Start()
    {
      
        dondeMiro = GetComponent<LookGameObject>();
        player = dondeMiro.objectToLookAt;
        canvasClient.SetActive(false);
        ChangeState(currentState);
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Wait:
                timeUI.fillAmount = currentTimeToZero / timeToWait;
                currentTimeToZero -= Time.deltaTime;

                if (currentTimeToZero <= 0)
                {
                    Unhappy();
                }
                break;
            case States.Huye:
                this.transform.position = Vector3.MoveTowards(this.transform.position, NextObjetivo, speed * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, puntosDeHuida.position) < 0.1f)
                {
                    ChangeState(States.Inicio);
                }
                break;
            case States.Inicio:
                this.transform.position = Vector3.MoveTowards(this.transform.position, NextObjetivo, speed * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, puntoInicio.position) < 0.1f)
                {
                    ChangeState(States.Puerta);
                }
                break;
            case States.Puerta:
                this.transform.position = Vector3.MoveTowards(this.transform.position, NextObjetivo, speed * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, puntoPuerta.position) < 0.1f)
                {
                    ChangeState(States.Mostrador);
                }
                break;
            case States.Mostrador:
                this.transform.position = Vector3.MoveTowards(this.transform.position, NextObjetivo, speed * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, puntosDeEspera.position) < 0.1f)
                {
                    ChangeState(States.Wait);
                }
                break;
            
            default:
                break;
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
        ChangeState(States.Huye);
    }

    public void Happy()
    {
        VSFX.instance.CreateParticleSystem(VSFX.instance.happyPS, this.transform.position+Vector3.zero, false);
        ChangeState(States.Huye);
    }

    public void ChangeState(States newState)
    {
        switch (currentState)
        {
            case States.Wait:
                canvasClient.SetActive(false);
                animator.SetBool("Walking", true);
                break;
            case States.Huye:
                break;
            case States.Puerta:
                break;
            case States.Mostrador:
                break;
            default:
                break;
        }

        switch (newState)
        {
            case States.Wait:
                canvasClient.SetActive(true);
                animator.SetBool("Walking", false);
                NewOrder();
                dondeMiro.objectToLookAt = player;
                break;
            case States.Huye:
                NextObjetivo = puntosDeHuida.position;
                dondeMiro.objectToLookAt = puntosDeHuida.gameObject;
                break;
            case States.Puerta:
                NextObjetivo = puntoPuerta.position;
                dondeMiro.objectToLookAt = puntoPuerta.gameObject;
                //doorOpening.Play();
                doorOpening.currentDoorState = AnimationDoor.doorState.OPENING;
                break;
            case States.Inicio:
                NextObjetivo = puntoInicio.position;
                dondeMiro.objectToLookAt = puntoInicio.gameObject;
                break;
            case States.Mostrador:
                NextObjetivo = puntosDeEspera.position;
                dondeMiro.objectToLookAt = puntosDeEspera.gameObject;
                break;
            default:
                break;
        }

        currentState = newState;
    }


}
