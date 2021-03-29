using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private MenuController instance;

    public bool soundOn;
    public bool musicOn;
    public bool notificationsOn;

    public GameObject[] languageObjectArray;
    public string[] languageStringArray;

    public string currentLanguage;
    int currentNum = 0;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        foreach (var item in languageObjectArray)
        {
            item.SetActive(false);
        }
        languageObjectArray[currentNum].SetActive(true);
        currentLanguage = languageStringArray[currentNum];
    }
    
    public void TwitterAcces()
    {
        Application.OpenURL("https://twitter.com/RollingCatGames");
    }
    
    public void RateAcces()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.area120.grasshopper&hl=es&gl=US");
    }

    public void ChangeLanguage()
    {
        foreach (var item in languageObjectArray)
        {
            item.SetActive(false);
        }

        currentNum++;
        if (currentNum>=languageObjectArray.Length)
        {
            currentNum = 0;
        }

        languageObjectArray[currentNum].SetActive(true);
        currentLanguage = languageStringArray[currentNum];
    }

    public void ChangeSound()
    {
        soundOn = !soundOn;
    }

    public void ChangeMusic()
    {
        musicOn = !musicOn;
    } 

    public void ChangeNot()
    {
        notificationsOn = !notificationsOn;
    }
}
