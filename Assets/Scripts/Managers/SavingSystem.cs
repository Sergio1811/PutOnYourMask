using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public static SavingSystem instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    void Start()
    {
        GameManager.instance.AddCoins(PlayerPrefs.GetInt("Coins") - GameManager.instance.coins);

    }

}
