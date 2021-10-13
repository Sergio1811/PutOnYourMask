using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersTutoControl : MonoBehaviour
{
    public GameObject[] tutorialCards;
    public GameObject manoP6;
    public GameObject manoP7_1;
    public GameObject manoP7_2;

    int currentCard;
    void Start()
    {
        if (PlayerPrefs.GetString(this.gameObject.name) == "Completed")
        {
            this.gameObject.SetActive(false);
            AccessControlManager.instance.currentState = AccessControlManager.GameState.Play;
        }
        else
        {
            this.gameObject.SetActive(true);
            tutorialCards[currentCard].SetActive(true);
            AccessControlManager.instance.currentState = AccessControlManager.GameState.Stopped;
        }

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
                PlayerPrefs.SetString(this.gameObject.name, "Completed");
                this.gameObject.SetActive(false);
                AccessControlManager.instance.currentState = AccessControlManager.GameState.Play;
                currentCard = 0;
                tutorialCards[currentCard].SetActive(true);
                manoP7_1.SetActive(false);
                manoP7_2.SetActive(false);
            }

            else
            {
                tutorialCards[currentCard].SetActive(true);

                if (currentCard == 5)
                {
                    manoP6.SetActive(true);
                }
                else if (currentCard == 6)
                {
                    manoP6.SetActive(false);
                    manoP7_1.SetActive(true);
                    manoP7_2.SetActive(true);
                }
            }
        }
    }
}
