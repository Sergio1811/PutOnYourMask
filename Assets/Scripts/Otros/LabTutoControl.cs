using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTutoControl : MonoBehaviour
{
    public GameObject[] tutorialCards;

    public GameObject manoP2_1;
    public GameObject manoP2_2;
    public GameObject manoP3;
    public GameObject manoP5;
    public GameObject manoP7;
    public GameObject manoP8;
    public GameObject manoP9;
    public GameObject manoP11;

    public GameObject doctor1;
    public GameObject doctor2;

    public GameObject canvasBotella;

    int currentCard;
    void Start()
    {
        if (PlayerPrefs.GetString(this.gameObject.name) == "Completed")
        {
            Completed();
        }
        else
        {
            tutorialCards[currentCard].SetActive(true);
            LabManager.instance.currentState = LabManager.GameState.Stopped;
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
                PlayerPrefs.GetString(this.gameObject.name, "Completed");
                Completed();
            }

            else
            {
                tutorialCards[currentCard].SetActive(true);

                if (currentCard == 1)
                {
                    manoP2_1.SetActive(true);
                    manoP2_2.SetActive(true);
                }
                else if (currentCard == 2)
                {
                    manoP2_1.SetActive(false);
                    manoP2_2.SetActive(false);
                    manoP3.SetActive(true);
                }
                else if (currentCard == 3)
                {
                    manoP3.SetActive(false);
                }
                else if (currentCard == 4)
                {
                    manoP5.SetActive(true);
                    canvasBotella.SetActive(false);
                }
                else if (currentCard == 5)
                {
                    manoP5.SetActive(false);
                }
                else if (currentCard == 6)
                {
                    //canvasBotella.SetActive(true);

                    manoP7.SetActive(true);
                }
                else if (currentCard == 7)
                {
                    manoP7.SetActive(false);
                    manoP8.SetActive(true);
                }
                else if (currentCard == 8)
                {
                    manoP8.SetActive(false);
                    manoP9.SetActive(true);
                }
                else if (currentCard == 9)
                {
                    manoP9.SetActive(false);
                }
                else if (currentCard == 10)
                {
                    manoP11.SetActive(true);

                }
                else if (currentCard == 11)
                {
                    manoP11.SetActive(false);
                }
            }
        }
    }
    public void Completed()
    {
        currentCard = 0;
        tutorialCards[currentCard].SetActive(true);
        this.gameObject.SetActive(false);
        doctor1.gameObject.SetActive(false);
        doctor2.gameObject.SetActive(false);
        LabManager.instance.currentState = LabManager.GameState.Play;
    }
}
