using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutoControl : MonoBehaviour
{
    public GameObject[] tutorialCards;
    public GameObject virus;
    public GameObject virusDeath;
    int currentCard;
    void Start()
    {
        tutorialCards[currentCard].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialCards[currentCard].SetActive(false);
            currentCard++;

            if (currentCard >= tutorialCards.Length)
            {
                PlayerPrefs.GetString(this.gameObject.name, "Completed");              
                Destroy(this.gameObject);
            }

            else
            {
                tutorialCards[currentCard].SetActive(true);

                if (currentCard == 1)
                {
                    virus.SetActive(true);
                }
                else if (currentCard == 2)
                {
                    virus.SetActive(false);
                    virusDeath.SetActive(true);
                }
                else if (currentCard == 3)
                {
                    virusDeath.SetActive(false);
                }
            }
        }
    }
}
