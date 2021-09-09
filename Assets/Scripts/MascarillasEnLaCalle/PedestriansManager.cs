using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PedestriansManager : MonoBehaviour
{
    public enum GameState { Play, Finish };
    public GameState currentState = GameState.Play;
    public static PedestriansManager instance;
    [Tooltip("In Seconds")] public int miniGameTime;
    float currentTimeMinigame;
    public TextMeshProUGUI secondsText;
    int currentValue = 6;
    GameObject characterPrefab;
    [HideInInspector] public Material maskOnMat;
    [HideInInspector] public Material maskOffMat;
    [HideInInspector] public Material yellowTraffic;

    public List<GameObject> pedestriansList;
    public float distanceToInfect;

    public GameObject[] trafficLights;

    public GameObject canvasFinal;
    [HideInInspector]
    public PunctuationCanvas canvasFinale;

    [Header("Data")]
    public int masked;
    public int nonMasked;
    public int infected;
    public int initialInfected;

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
        if (instance == null)
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
        canvasFinale = canvasFinal.GetComponentInChildren<PunctuationCanvas>();

        pedestriansList = ObjectPooler.SharedInstance.pooledObjects;
        characterPrefab = Resources.Load("Prefabs/Character") as GameObject;
        maskOffMat = Resources.Load("Materials/MaskOff") as Material;
        maskOnMat = Resources.Load("Materials/MaskOn") as Material;
        yellowTraffic = Resources.Load("Materials/YellowTraffic") as Material;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Play)
        {
            #region TimeControl
            currentTimeMinigame += Time.deltaTime;
            int currentMinutes = (int)(miniGameTime - currentTimeMinigame) / 60;
            int currentSeconds = (int)(miniGameTime - currentTimeMinigame) - (currentMinutes * 60);

            if (currentSeconds >= 10)
            {
                secondsText.text = currentMinutes + " : " + currentSeconds;
            }
            else
            {
                secondsText.text = currentMinutes + " : 0" + currentSeconds;
            }

            if ((Time.time / 10) >= currentValue)
            {
                /* StartCoroutine(fadePitch(GameManager.instance.audioManager.pitch+0.05f));
                 currentValue++;*/
            }
            if (currentTimeMinigame > miniGameTime)
            {
                Finish();
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
    }

    IEnumerator fadePitch(float next)
    {

        while (GameManager.instance.audioManager.pitch < next)
        {
            GameManager.instance.audioManager.pitch = Mathf.Lerp(GameManager.instance.audioManager.pitch, next, 0.025f);

            yield return null;
        }

    }

    public void Finish()
    {
        //calculos de puntuacion
        if (masked + nonMasked - (infected - initialInfected) >= 0.5f)
        {
            //buena partida
        }
        else
        {
            //mala partida
        }
        currentState = GameState.Finish;
        //calculos de puntuacion

        canvasFinale.coins = "100";
        GameManager.instance.AddCoins(100);

        canvasFinale.iniPercentage = GameManager.instance.virusPercentage.ToString();
        canvasFinale.finalPercentage = (GameManager.instance.virusPercentage - 20).ToString();
        GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage - 20, 0, 100);
        PlayerPrefs.SetInt("Virus", (int)GameManager.instance.virusPercentage);

        canvasFinal.SetActive(true);
    }
}
