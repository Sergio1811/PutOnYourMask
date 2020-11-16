﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermometroController : MonoBehaviour
{
    public Text temperatureText;//Display text temp
    [Tooltip("Time the thermometer will take to measure")] public float timeToMeasure;
    float currentTimeMeasuring;//timeController

    public Slider sliderTemp;//Display loading time

    bool measured = false;//Is Temperature measured

    void Update()
    {
        ThermometreInHead();
    }

    public void MeasureTemperature()//Actions need to be made for measure temperature
    {
        int temp = AccessControlManager.instance.currentCharTemp;//get temp
        AssignToText(temp);//assign to therm display
        measured = true;
    }

    public void AssignToText(int temp)
    {
        temperatureText.text =  temp + "ºC";
    }

    public void ThermometreInHead()
    {//Cast Ray forward the thermometre (from the pivot actually)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            if (hit.collider.CompareTag("CharsHead"))//Detect therm in char head
            {
                print("chars");
                if (!measured)//If not finished add time and slider
                {
                    currentTimeMeasuring += Time.deltaTime;
                    sliderTemp.value = currentTimeMeasuring / timeToMeasure;

                    if (currentTimeMeasuring >= timeToMeasure)//If finalized call function
                    {
                        MeasureTemperature();
                    }
                }
            }

            else if (currentTimeMeasuring > 0)
            {
                //Reset thermometre
                currentTimeMeasuring = 0;
                sliderTemp.value = 0;
                measured = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward);
    }
}
