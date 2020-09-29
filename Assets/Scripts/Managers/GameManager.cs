using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioManager;

    float miniGameTime = 120;
    float currentMiniGameTime;

    private void Awake()
    {

        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        currentMiniGameTime += Time.deltaTime;

        if(currentMiniGameTime>= miniGameTime)
        {
            SceneController.instance.ChargeMainMenu();
        }
    }
}
