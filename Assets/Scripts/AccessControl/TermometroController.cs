using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TermometroController : MonoBehaviour
{
    public TextMeshProUGUI temperatureText;//Display text temp
    [Tooltip("Time the thermometer will take to measure")] public float timeToMeasure;
    float currentTimeMeasuring;//timeController

    public Image imageTempCharge;//Display loading time

    bool measured = false;//Is Temperature measured

    private void Start()
    {
        Restart();
    }
    void Update()
    {
        ThermometreInHead();
    }

    public void MeasureTemperature()//Actions need to be made for measure temperature
    {
        int temp = AccessControlManager.instance.currentCharTemp;//get temp
        AssignToText(temp);//assign to therm display
        measured = true;
        VSFX.instance.PlayAudio(VSFX.instance.thermometerSound);
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

                if (!measured)//If not finished add time and slider
                {
                    currentTimeMeasuring += Time.deltaTime;
                    imageTempCharge.fillAmount = currentTimeMeasuring / timeToMeasure;

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
                imageTempCharge.fillAmount = 0;
                measured = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward);
    }

    public void Restart()
    {
        currentTimeMeasuring = 0;
        imageTempCharge.fillAmount = 0;
        temperatureText.text = "----";
        measured = false;
    }
}
