using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTutoControl : MonoBehaviour
{
    public GameObject[] tutorialCards;

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
            }
        }
    }
}
