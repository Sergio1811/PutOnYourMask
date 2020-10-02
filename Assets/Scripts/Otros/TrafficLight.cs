using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum trafficLightState { Green, Red, Yellow };

    public trafficLightState currentState;
    trafficLightState previousState;

    public Renderer lightGO;

    float currentTime;

    public float timeToChange;
    public float yellowTime;

    private void Start()
    {
      if(currentState == trafficLightState.Red) {  lightGO.material = PedestriansManager.instance.maskOffMat; }  
      else if(currentState==trafficLightState.Green) { lightGO.material= PedestriansManager.instance.maskOnMat;  }  
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToChange)
        {
            StartCoroutine("YellowChange");
        }

    }

    private void ChangeColor()
    {
        currentTime = 0;

        if (previousState== trafficLightState.Red)
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

    private IEnumerator YellowChange()
    {
        previousState = currentState;
        currentTime = 0;
        currentState = trafficLightState.Yellow;
        lightGO.material = PedestriansManager.instance.yellowTraffic;
        yield return new WaitForSeconds(yellowTime);
        ChangeColor();
    }
}
