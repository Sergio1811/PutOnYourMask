using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedestriansManager : MonoBehaviour
{
    public static PedestriansManager instance;
    [Tooltip("In Seconds")] public int miniGameTime;
    public Text secondsText;
    int currentValue=6;
    GameObject characterPrefab;
    [HideInInspector]public Material maskOnMat;
    [HideInInspector]public Material maskOffMat;

    public List<GameObject> pedestriansList;
    public float distanceToInfect;

    public GameObject[] trafficLights;

    public enum PedestrianType
    {
        Masked,
        Non_Masked,
        Infected,
        Runner,
        Runner_Infected
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        characterPrefab = Resources.Load("Prefabs/Character") as GameObject;
        maskOffMat = Resources.Load("Materials/MaskOff") as Material;
        maskOnMat = Resources.Load("Materials/MaskOn") as Material;
    }

    // Update is called once per frame
    void Update()
    {
        #region TimeControl
        int currentMinutes = (int)(miniGameTime - Time.time) / 60;
        int currentSeconds = (int)(miniGameTime - Time.time) - (currentMinutes * 60);

        if (currentSeconds >= 10)
        {
            secondsText.text = currentMinutes + " : " + currentSeconds;
        }
        else
        {
            secondsText.text = currentMinutes + " : 0" + currentSeconds;
        }

        if((Time.time/10)>=currentValue)
        {
            StartCoroutine(fadePitch(GameManager.instance.audioManager.pitch+0.05f));
            currentValue++;
        }
        #endregion

        #region ClickOnChar
        if (InputManager.Instance.WhatAmIClicking() != null)
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Character"))
            {
                GameObject saveGO = InputManager.Instance.WhatAmIClicking();

                if (saveGO.transform.parent.GetComponent<Pedestrians>().thisType == PedestrianType.Masked || saveGO.transform.parent.GetComponent<Pedestrians>().thisType == PedestrianType.Runner || saveGO.transform.parent.GetComponent<Pedestrians>().thisType == PedestrianType.Runner_Infected)
                {
                    int random = Random.Range(0, 1);
                    switch (random)
                    {
                        case 0:
                            Debug.Log("That's a good guy or a runner");
                            break;
                        case 1:
                            Debug.Log("That's a good girl or a runner");
                            break;
                        default:
                            break;
                    }
                }
                else
                { 
                    saveGO.transform.parent.GetComponent<Pedestrians>().MaskOn();
                }
            }
        }
        #endregion
    }

    IEnumerator fadePitch(float next)
    {

        while (GameManager.instance.audioManager.pitch < next)
        {
            GameManager.instance.audioManager.pitch = Mathf.Lerp(GameManager.instance.audioManager.pitch, next, 0.025f);

            yield return null;
        }

    }
}
