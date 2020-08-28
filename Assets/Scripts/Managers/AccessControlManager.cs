using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessControlManager : MonoBehaviour
{
    public static AccessControlManager instance;

    public GameObject Termometro;

    public int currentTemp;

    public enum Options {Success, Fail};
    public Options Result;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.WhatAmIClicking() != null)
        {
            GoInButton();
            GoOutButton();
        }

        InputManager.Instance.DragAndDrop(Termometro, true);
    }

    void GoInButton()
    {
        if (InputManager.Instance.WhatAmIClicking().CompareTag("Go In"))
        {
            //Move Char Forward
            CheckDecision(true);
            SpawnChar();

            //New info in PC
            Debug.Log("Go In button, result:" + Result);
        }
    }

    void GoOutButton()
    {
        if (InputManager.Instance.WhatAmIClicking().CompareTag("Go Out"))
        {
            //Move Char Back
            CheckDecision(false);
            SpawnChar();

            //New info in PC
            Debug.Log("Go Out button, result:" + Result);
        }
    }

    public void GetCurrentCharTemp()
    {
         currentTemp = Random.Range(35, 40);
    }

    public void CheckDecision(bool passed)
    {
        if (passed)
        {
            if (currentTemp >= 37)
                Result = Options.Fail;
            else
                Result = Options.Success;
        }
        else
        {
            if (currentTemp >= 37)
                Result = Options.Success;
            else
                Result = Options.Fail;
        }
    }

    public void SpawnChar()
    {
        GetCurrentCharTemp();
    }

    //Disable buttons
}
