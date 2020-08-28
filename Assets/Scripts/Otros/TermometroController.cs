using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermometroController : MonoBehaviour
{
    public Text temperatureText;
    public float timeToMeasure;
    float currentTimeMeasuring;
    public Slider sliderTemp;
    bool measured =false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.forward, out hit))
        {
            if(hit.collider.CompareTag("CharsHead") )
            {
                if (!measured)
                {
                    currentTimeMeasuring += Time.deltaTime;
                    sliderTemp.value = currentTimeMeasuring / timeToMeasure;

                    if (currentTimeMeasuring >= timeToMeasure)
                        MeasureTemperature();
                }
            }
            else if(currentTimeMeasuring>0)
            {
                currentTimeMeasuring = 0;
                sliderTemp.value = 0;
                measured = false;
            }
        }

    }

    public void MeasureTemperature()
    {
        int temp = AccessControlManager.instance.currentTemp;
        AssignToText(temp);
        measured = true;
    }

    public void AssignToText(int temp)
    {
        temperatureText.text = "Temp: " + temp + "ºC";
    }
}
