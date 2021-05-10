using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessControlManager : MonoBehaviour
{
    public enum GameState { Play, Finish };
    public GameState currentState = GameState.Play;
    public static AccessControlManager instance;

    public GameObject Termometro;
    public GameObject Palanca;
    public GameObject Buttons;
    public GameObject Calcetin;

    public GameObject _2DMaskPrefab;

    public Canvas myCanvas;

    public string[] symptoms = new string[] { "Tos seca", "Dolor de pecho", "Cansancio", "Dificultad para respirar " };
    public enum allSymptoms { DolorGarganta, Anginas, Diarrea, Conjuntivitis, Migraña, MBoce, ErupcionesCutaneas, DolorPiernas, Influencer, PerdidaVision, MolestiasCervicales, EBoy, Negacionista, Cirrosis, Celiaco, Vegano, Diabetes, TerraPlanista, NONE, RealSymptom };
    List<allSymptoms> currentRandomCharSymptoms = new List<allSymptoms>();
    [Range(0.0f, 1.0f)] public float ratioNoSymp; //Probability to have a symptom

    public int currentCharTemp;//var de la temperatura del prox personaje
    public int minRandomTemp;//min temp random
    public int maxRandomTemp;//max temp tandom
    [Tooltip("Temperatura a partir de la que no pasa")] public int tempToFail;

    [HideInInspector] public bool currentCharMask;//var de si lleva mask el current char
    [Tooltip("Probabilidad de que el personaje lleve máscara en decimal")] [Range(0, 1)] public float maskProbability;//probabilidad de que lleven mascara

    [HideInInspector] public bool currentCharCanSmell;//var de si puede oler el current char
    [Tooltip("Probabilidad de que el personaje pueda oler en decimal")] [Range(0, 1)] public float canSmellProbability;//probabilidad de que puedan oler

    public enum ButtonState { Able, Disable };
    public ButtonState currentButtonState;

    public Transform[] movementPoints; //Char movement points

    int currentWaypoint = 0;

    public Text sympsText;

    public GameObject[] charsToCheck; //CharsPrefabs

    GameObject currentChar;

    bool currentCharCanPass = true;

    public float buttonsCooldown;

    bool maskIsOnlyProblem = false;

    public GameObject panelPostit;

    public GameObject canvasFinal;
    [HideInInspector]
    public PunctuationCanvas canvasFinale;

    [Header("Stats")]
    public int howManyFailed;
    public int howManyPassed;

    public float timeMinigame;
    float currentTimeMinigame;
    public TextMeshProUGUI timeText;
    int currentValue = 6;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        canvasFinale = canvasFinal.GetComponentInChildren<PunctuationCanvas>();

        SpawnChar();

        GetCurrentCharTemp();
        currentButtonState = ButtonState.Able;

        if (maxRandomTemp <= minRandomTemp)
        {
            maxRandomTemp += 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Play)
        {
            //Control juego
            if (InputManager.Instance.WhatAmIClicking() != null) //if click on any button
            {
                OpenPostit();
                GoInButton();
                StartCoroutine(GoOutButton());
                PalancaOlor();
                PickMask();
            }

            InputManager.Instance.DragAndDrop(Termometro, true);

            //Condiciones derrota y victoria
            TimeControl();

            if (howManyFailed >= 3)
            {
                Finish();
            }
        }
    }

    void GoInButton()//Let char Pass button logic
    {
        if (currentButtonState == ButtonState.Able)//maybe on updsate
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Go In"))
            {
                MoveNextPoint();
                Buttons.GetComponent<Animation>().clip = Buttons.GetComponent<Animation>().GetClip("BotonDer");
                Buttons.GetComponent<Animation>().Play();
                StartCoroutine("ButtonCooldown");
                CheckDecision(true);
                SpawnChar();
                //New info in PC

            }
        }
    }

    IEnumerator GoOutButton()//Denegate permission Button logic
    {
        if (currentButtonState == ButtonState.Able)
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Go Out"))
            {
                Buttons.GetComponent<Animation>().clip = Buttons.GetComponent<Animation>().GetClip("BotonIzq");
                Buttons.GetComponent<Animation>().Play();
                StartCoroutine("ButtonCooldown");

                int rnd = Random.Range(0, 3);
                if (rnd == 0)
                {
                    currentChar.GetComponent<CharsPPMovement>().faceControl.FaceAnim("AngryFace");
                }
                else if (rnd == 1)
                {
                    StartCoroutine(currentChar.GetComponent<CharsPPMovement>().faceControl.SurpriseFace(4));
                }
                else
                {
                    currentChar.GetComponent<CharsPPMovement>().faceControl.FaceAnim("SadFace");
                }

                yield return new WaitForSeconds(1);

                MoveBackPoint();


                CheckDecision(false);
                SpawnChar();
                //New info in PC
            }
        }
    }

    public void OpenPostit()
    {
        if (InputManager.Instance.WhatAmIClicking().CompareTag("Postit"))
        {
            panelPostit.SetActive(true);
        }
    }

    public void SpawnChar()//Logic needed when Character spawns
    {
        if (charsToCheck.Length > 0 && movementPoints.Length > 0)
        {
            currentCharCanPass = true;
            currentCharMask = false;
            int tempRnd = Random.Range(0, charsToCheck.Length - 1);


            currentChar = Instantiate(charsToCheck[tempRnd], movementPoints[0].position, charsToCheck[tempRnd].transform.rotation);
            currentChar.GetComponent<LookGameObject>().objectToLookAt = movementPoints[1].gameObject;
            currentChar.GetComponent<CharsPPMovement>().waypoint = movementPoints[1];
            StartCoroutine("LookPlayer");
            GetCurrentCharMask();
            GetCurrentCharSymptoms();
            GetCurrentCharCanSmell();
            GetCurrentCharTemp();

            AccesCanvasControler.instance.ChangeName();

        }
    }

    public void CheckDecision(bool passed)
    {
        if (passed != currentCharCanPass)
        {
            AccesCanvasControler.instance.StartCoroutine("Cross");
            howManyFailed++;
            Debug.Log("Failed");
        }
        else
        {
            VSFX.instance.PlayAudio(VSFX.instance.inaudibleSound);
            AccesCanvasControler.instance.StartCoroutine("Tick");
            howManyPassed++;
            Debug.Log("Success");
        }
    }

    public void GetCurrentCharTemp()//Get temp according threshold
    {
        currentCharTemp = Random.Range(minRandomTemp, maxRandomTemp);
        if (currentCharTemp >= tempToFail)
        {
            currentCharCanPass = false;
            maskIsOnlyProblem = false;
            Debug.Log("I change it to false, temp");

        }
    }

    public void GetCurrentCharMask() //Get mask bool according to probability
    {
        currentCharMask = Random.value > (1 - maskProbability);
        if (!currentCharMask)
        {
            currentChar.GetComponent<CharsPPMovement>().mask.SetActive(false);
            if (currentCharCanPass)
            {
                maskIsOnlyProblem = true;
                Debug.Log("I change it to false, mask");
                currentCharCanPass = false;

            }
        }
    }

    public void GetCurrentCharCanSmell()
    {
        currentCharCanSmell = Random.value > (1 - canSmellProbability);
        if (!currentCharCanSmell)
        {
            currentCharCanPass = false;
            maskIsOnlyProblem = false;
            Debug.Log("I change it to false, smell");

        }
    }

    public void GetCurrentCharSymptoms()
    {
        List<string> tempRealSymp = new List<string>(symptoms);

        if (currentRandomCharSymptoms.Count > 0)
        {
            currentRandomCharSymptoms.Clear();
        }

        int howManySymps = Random.Range(1, 4);

        AccesCanvasControler.instance.listSymptoms.text = "";

        for (int i = 0; i < howManySymps; i++)
        {

            float randomValue = Random.value;
            if (randomValue > (1 - ratioNoSymp))
            {
                GetRealSymptom(tempRealSymp);
                currentCharCanPass = false;
                maskIsOnlyProblem = false;
                Debug.Log("I change it to false, symp");

            }
            else
            {
                GetRandomSymptom();
            }
        }

        foreach (var item in currentRandomCharSymptoms)
        {
            if (item.ToString() == "NONE")
                break;
            else if (item.ToString() != "RealSymptom")
                AccesCanvasControler.instance.listSymptoms.text += "- " + item.ToString() + "\n";

        }
    }

    public void GetRandomSymptom()
    {
        System.Random rnd = new System.Random();

        allSymptoms tempSymptom = (allSymptoms)rnd.Next(System.Enum.GetNames(typeof(allSymptoms)).Length);

        while (tempSymptom == allSymptoms.RealSymptom || currentRandomCharSymptoms.Contains(tempSymptom))
        {
            rnd = new System.Random();

            tempSymptom = (allSymptoms)rnd.Next(System.Enum.GetNames(typeof(allSymptoms)).Length);
        }
        currentRandomCharSymptoms.Add(tempSymptom);
    }

    public void GetRealSymptom(List<string> l_Symptoms)
    {
        currentRandomCharSymptoms.Add(allSymptoms.RealSymptom);
        int rnd = Random.Range(0, l_Symptoms.Count);
        string tempString = l_Symptoms[rnd];
        l_Symptoms.RemoveAt(rnd);

        AccesCanvasControler.instance.listSymptoms.text += "- " + tempString + "\n";

    }

    public void MoveNextPoint()
    {
        currentChar.GetComponent<CharsPPMovement>().waypoint = movementPoints[2];
        currentChar.GetComponent<CharsPPMovement>().voted = true;
        currentChar.GetComponent<LookGameObject>().objectToLookAt = movementPoints[2].gameObject;
    }
    public void MoveBackPoint()
    {
        currentChar.GetComponent<CharsPPMovement>().waypoint = movementPoints[0];
        currentChar.GetComponent<CharsPPMovement>().voted = true;
        currentChar.GetComponent<LookGameObject>().objectToLookAt = movementPoints[0].gameObject;
    }


    public IEnumerator ButtonCooldown()//Coroutine interaction between able/disable buttons
    {
        ShowButtonsDisable();
        yield return new WaitForSecondsRealtime(buttonsCooldown);
        ShowButtonsAble();
    }

    public void ShowButtonsDisable()//Disbale buttons after use
    {

        currentButtonState = ButtonState.Disable;
        //Estetica
    }

    public void ShowButtonsAble()//Activate buttons after CD
    {

        currentButtonState = ButtonState.Able;
        //Estetica
    }

    public IEnumerator LookPlayer()
    {
        yield return new WaitForSecondsRealtime(1.25f);
        currentChar.GetComponent<LookGameObject>().objectToLookAt = gameObject;
    }

    public void PalancaOlor()
    {
        if (InputManager.Instance.WhatAmIClicking().CompareTag("PalancaOlor"))
        {
            VSFX.instance.PlayAudio(VSFX.instance.leverSound);
            Palanca.GetComponent<Animation>().Play();
            Calcetin.GetComponent<Animation>().Play();
            if (currentCharCanSmell)
            {
                currentChar.GetComponent<CharsPPMovement>().SmellActivate();
            }
        }
    }

    public void PickMask()
    {
        if (InputManager.Instance.WhatAmIClicking().CompareTag("CajasMascarillas"))
        {
            Instantiate(_2DMaskPrefab, myCanvas.transform);
            //Activtae calcetin
            VSFX.instance.PlayAudio(VSFX.instance.clicSound);
        }
    }

    public void maskOn()
    {
        if (maskIsOnlyProblem)
        {
            currentCharCanPass = true;
            VSFX.instance.PlayAudio(VSFX.instance.putMaskSound);
        }
    }

    public void TimeControl()
    {
        currentTimeMinigame += Time.deltaTime;
        int currentMinutes = (int)(timeMinigame - currentTimeMinigame) / 60;
        int currentSeconds = (int)(timeMinigame - currentTimeMinigame) - (currentMinutes * 60);

        if (currentSeconds >= 10)
        {
            timeText.text = "0" + currentMinutes + ":" + currentSeconds;
        }
        else
        {
            timeText.text = "0" + currentMinutes + ":0" + currentSeconds;
        }

        if ((Time.time / 10) >= currentValue)
        {
            // StartCoroutine(fadePitch(GameManager.instance.audioManager.pitch + 0.05f));
            currentValue++;
        }

        if (currentTimeMinigame > timeMinigame)
        {
            Finish();
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
        currentState = GameState.Finish;
        //calculos de puntuacion

        canvasFinale.coins = "100";
        GameManager.instance.AddCoins(100);

        canvasFinale.iniPercentage = GameManager.instance.virusPercentage.ToString();
        canvasFinale.finalPercentage = (GameManager.instance.virusPercentage - 20).ToString();
        GameManager.instance.virusPercentage -= 20;
        canvasFinal.SetActive(true);
    }

}
