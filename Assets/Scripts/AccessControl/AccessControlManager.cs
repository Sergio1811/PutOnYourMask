using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessControlManager : MonoBehaviour
{
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoInButton();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GoOutButton();
        }

        if (InputManager.Instance.WhatAmIClicking() != null) //if click on any button
        {
            GoInButton();
            GoOutButton();
            PalancaOlor();
            PickMask();
        }

        InputManager.Instance.DragAndDrop(Termometro, true);
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

    void GoOutButton()//Denegate permission Button logic
    {
        if (currentButtonState == ButtonState.Able)
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Go Out"))
            {
                MoveBackPoint();
                Buttons.GetComponent<Animation>().clip = Buttons.GetComponent<Animation>().GetClip("BotonIzq");
                Buttons.GetComponent<Animation>().Play();
                StartCoroutine("ButtonCooldown");
                
                CheckDecision(false);
                SpawnChar();
                //New info in PC

            }
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

            Debug.Log("Failed");
        }
        else
        {
            AccesCanvasControler.instance.StartCoroutine("Tick");

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
        }
    }

    public void maskOn()
    {
        if (maskIsOnlyProblem)
        {
            currentCharCanPass = true;
        }
    }

}
