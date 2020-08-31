using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessControlManager : MonoBehaviour
{
    public static AccessControlManager instance;

    public GameObject Termometro;

    public int currentCharTemp;//var de la temperatura del prox personaje
    public int minRandomTemp;//min temp random
    public int maxRandomTemp;//max temp tandom
    [Tooltip("Temperatura a partir de la que no pasa")] public int tempToFail;

    public bool currentCharMask;//var de si lleva mask el current char
    [Tooltip("Probabilidad de que el personaje lleve máscara en decimal")] [Range(0, 1)] public float maskProbability;//probabilidad de que lleven mascara

    public enum Options { Success, Fail };
    public Options result;
    public enum ButtonState { Able, Disable };
    public ButtonState currentButtonState;

    public Transform[] movementPoints; //Char movement points

    public GameObject[] charsToCheck;//CharsPrefabs

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
        Invoke("SpawnChar", 3);

        currentButtonState = ButtonState.Able;

        if (maxRandomTemp <= minRandomTemp)
        {
            maxRandomTemp += 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.WhatAmIClicking() != null) //if click on any button
        {
            GoInButton();
            GoOutButton();
        }

        InputManager.Instance.DragAndDrop(Termometro, true);
    }

    void GoInButton()//Let char Pass button logic
    {
        if (currentButtonState == ButtonState.Able)//maybe on updsate
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Go In"))
            {
                StartCoroutine("ButtonCooldown");
                MovementCharPassed();
                CheckDecision(true);
                SpawnChar();

                //New info in PC
                Debug.Log("Go In button, result:" + result);
            }
        }
    }

    void GoOutButton()//Denegate permission Button logic
    {
        if (currentButtonState == ButtonState.Able)
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Go Out"))
            {
                StartCoroutine("ButtonCooldown");
                MovementCharNotPassed();
                CheckDecision(false);
                SpawnChar();

                //New info in PC
                Debug.Log("Go Out button, result:" + result);
            }
        }
    }

    public void GetCurrentCharTemp()//Get temp according threshold
    {
        currentCharTemp = Random.Range(minRandomTemp, maxRandomTemp);
    }

    public void GetCurrentCharMask() //Get mask bool according to probability
    {
        currentCharMask = Random.value > (1 - maskProbability);
    }

    public Options CheckDecision(bool passed) //MUY MEJORABLE, every if-else is a check, if bad decision return fail if not next
    {                                         //check until finish and give success or next check
        CheckTemperature(passed);

        if (result == Options.Fail)
        {
            return result;
        }
        else
        {
            CheckMask(passed);
        }

        if (result == Options.Fail)
        {
            return result;
        }
        else
        {
            //NextCheck

            return result;
        }
    }

    public void SpawnChar()//Logic needed when Character spawns
    {
        if (charsToCheck.Length > 0 && movementPoints.Length > 0)
        {
            int tempRnd = Random.Range(0, charsToCheck.Length - 1);
            GetCurrentCharTemp();
            GetCurrentCharMask();
            Instantiate(charsToCheck[tempRnd], movementPoints[0].position, charsToCheck[tempRnd].transform.rotation);
        }
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

    public void MovementCharPassed()//Move char forward
    {
        //Movement
    }

    public void MovementCharNotPassed()//Move char back
    {
        //Movement
    }

    public void CheckTemperature(bool passed)//Check if temperature was on threshold and give result according on player input
    {
        if (passed)
        {
            if (currentCharTemp >= tempToFail)
            {
                result = Options.Fail;
            }
            else
            {
                result = Options.Success;
            }
        }
        else
        {
            if (currentCharTemp >= tempToFail)
            {
                result = Options.Success;
            }
            else
            {
                result = Options.Fail;
            }
        }
    }

    public void CheckMask(bool passed) //Check if mask was on the char and give result according on player input
    {
        if (passed)
        {
            if (currentCharMask)
            {
                result = Options.Success;
            }
            else
            {
                result = Options.Fail;
            }
        }
        else
        {
            if (currentCharMask)
            {
                result = Options.Fail;
            }
            else
            {
                result = Options.Success;
            }
        }
    }
}
