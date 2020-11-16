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

    public GameObject _2DMaskPrefab;

    public Canvas myCanvas;

    public string[] symptoms = new string[] { "Tos seca", "Dolor de pecho", "Cansancio", "Dificultad para respirar " };
    public enum allSymptoms { DolorGarganta, Anginas, Diarrea, Conjuntivitis, Migraña, MBoce, ErupcionesCutaneas, DolorPiernas, Influencer, PerdidaVision, MolestiasCervicales, EBoy, Negacionista, Cirrosi, Celiaco, Vegano, Diabetes, TerraPlanista, NONE, RealSymptom };
    List<allSymptoms> currentRandomCharSymptoms;
    [Range(0.0f, 1.0f)] public float ratioNoSymp; //Probability to have a symptom

    [HideInInspector] public int currentCharTemp;//var de la temperatura del prox personaje
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
                Debug.Log("Go In button, result:");
                CheckDecision(true);
                SpawnChar();
                GetCurrentCharTemp();
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
                Debug.Log("Go Out button, result:");
                CheckDecision(false);
                SpawnChar();
                GetCurrentCharTemp();
                //New info in PC

            }
        }
    }

    public void SpawnChar()//Logic needed when Character spawns
    {
        if (charsToCheck.Length > 0 && movementPoints.Length > 0)
        {
            int tempRnd = Random.Range(0, charsToCheck.Length - 1);
            GetCurrentCharTemp();
            currentChar = Instantiate(charsToCheck[tempRnd], movementPoints[0].position, charsToCheck[tempRnd].transform.rotation);
            currentChar.GetComponent<LookGameObject>().objectToLookAt = movementPoints[1].gameObject;
            currentChar.GetComponent<CharsPPMovement>().waypoint = movementPoints[1];
            StartCoroutine("LookPlayer");
            GetCurrentCharMask();
            currentCharCanPass = true;

        }
    }

    public void CheckDecision(bool passed)
    {
        if (passed != currentCharCanPass)
        {
            Debug.Log("Failed");
        }
        else
        {
            Debug.Log("Success");
        }
    }

    public void GetCurrentCharTemp()//Get temp according threshold
    {
        currentCharTemp = Random.Range(minRandomTemp, maxRandomTemp);
        if (currentCharTemp >= tempToFail)
        {
            currentCharCanPass = false;
        }
    }

    public void GetCurrentCharMask() //Get mask bool according to probability
    {
        currentCharMask = Random.value > (1 - maskProbability);
        if (!currentCharMask)
        {
            currentChar.GetComponent<CharsPPMovement>().mask.SetActive(false);
            currentCharCanPass = false;
        }
    }

    public void GetCurrentCharCanSmell()
    {
        currentCharCanSmell = Random.value > (1 - canSmellProbability);
        if (!currentCharCanSmell)
        {
            currentCharCanPass = false;
        }
    }

    public void GetCurrentCharSymptoms()
    {
        currentRandomCharSymptoms.Clear();

        float randomValue = Random.value;
        if (randomValue > (1 - ratioNoSymp))
        {
            string realSymptom = GetRealSymptom();
            currentCharCanPass = false;
        }
        else
        {
            GetRandomSymptom();
        }
    }

    public void GetRandomSymptom()
    {
        System.Random rnd = new System.Random();

        currentRandomCharSymptoms.Add((allSymptoms)rnd.Next(System.Enum.GetNames(typeof(allSymptoms)).Length));

        while (currentRandomCharSymptoms.Contains(allSymptoms.RealSymptom))
        {
            currentRandomCharSymptoms.Clear();

            rnd = new System.Random();

            currentRandomCharSymptoms.Add((allSymptoms)rnd.Next(System.Enum.GetNames(typeof(allSymptoms)).Length));
        }
    }

    public string GetRealSymptom()
    {
        currentRandomCharSymptoms.Add(allSymptoms.RealSymptom);

        return symptoms[Random.Range(0, symptoms.Length)];
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
        Debug.Log("Button Disable");

        currentButtonState = ButtonState.Disable;
        //Estetica
    }

    public void ShowButtonsAble()//Activate buttons after CD
    {
        Debug.Log("Button Able");

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
            //Activtae calcetin
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

}
