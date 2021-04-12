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

    public enum Clothing { Head, Shirt, Pants, Shoes};
    public Clothing currentCloth = Clothing.Shirt;

    bool maskOn = false;
    public GameObject maskChar;
    public GameObject canvasItem;

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
        print("hi");
        Application.OpenURL("http://www.twitter.com/RollingCatGames");

        print("hi2");

        Application.OpenURL("twitter:///user?screen_name=RollingCatGames");

    }

    public void RateAcces()
    {
        Application.OpenURL("https://play.google.com/apps/internaltest/4699336267926481719");
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

    public void RandomOutfit()
    {

    }

    public void NextCloth()
    {

    }
    
    public void PrevCloth()
    {

    }

    public void MaskChange()
    {
        maskOn = !maskOn;

        if(maskOn)
        maskChar.SetActive(true);

        else
        maskChar.SetActive(true);
    }

    public void PurchaseItem()
    {
        canvasItem.SetActive(true);
        //rellenar datos
    }

    public void SelectCloth(Clothing l_Cloth)
    {
        currentCloth = l_Cloth;
    }
}
