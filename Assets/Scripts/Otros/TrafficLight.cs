using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum trafficLightState { Green, Red };

    public trafficLightState currentState;

    public Renderer lightGO;

    float currentTime;

    public float timeToChange;

    private void Start()
    {
      if(currentState == trafficLightState.Red) {  lightGO.material = PedestriansManager.instance.maskOffMat; }  
      else if(currentState==trafficLightState.Green) { lightGO.material= PedestriansManager.instance.maskOnMat;  }  
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToChange)
            ChangeColor();

    }

    private void ChangeColor()
    {
        currentTime = 0;

        if(currentState== trafficLightState.Red)
        {
            currentState = trafficLightState.Green;
            lightGO.material = PedestriansManager.instance.maskOnMat;

        }

        else
        {
            currentState = trafficLightState.Red;
            lightGO.material = PedestriansManager.instance.maskOffMat;
        }
    }
}
