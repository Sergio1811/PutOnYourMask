using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public enum Clothing { Head, Shirt, Pants, Shoes, Mask, Special };
    public Sprite[] clothIcons;
    public Clothing currentCloth = Clothing.Shirt;
    public Animation m_DropdownAnimation;
    public AnimationClip openAnim;
    public AnimationClip closedAnim;
    bool ddOpened = false;
    public Image currentIcon;
    bool maskOn = false;
    public GameObject maskChar;
    public GameObject canvasItem;
    

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

        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        foreach (var item in languageObjectArray)
        {
            item.SetActive(false);
        }
        currentLanguage = GameManager.instance.language;

        switch (currentLanguage)
        {
            case "EN":
                
                break;
            case "ES":
                currentNum = 1;
                break;
            case "CA":
                currentNum = 2;
                break;
            default:
                break;
        }

        currentLanguage = languageStringArray[currentNum];
        languageObjectArray[currentNum].SetActive(true);
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
        if (currentNum >= languageObjectArray.Length)
        {
            currentNum = 0;
        }

        

        languageObjectArray[currentNum].SetActive(true);
        currentLanguage = languageStringArray[currentNum];
        GameManager.instance.language = currentLanguage;
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

        if (maskOn)
            maskChar.SetActive(true);

        else
            maskChar.SetActive(false);
    }

    public void PurchaseItem()
    {
        canvasItem.SetActive(true);
        //rellenar datos
    }


    public void SelectCloth(int l_Cloth)
    {
        switch (l_Cloth)
        {
            case 0:
                currentCloth = Clothing.Head;
                currentIcon.sprite = clothIcons[0];
                break;

            case 1:
                currentCloth = Clothing.Mask;
                currentIcon.sprite = clothIcons[1];
                break;

            case 2:
                currentCloth = Clothing.Shirt;
                currentIcon.sprite = clothIcons[2];
                break;

            case 3:
                currentCloth = Clothing.Pants;
                currentIcon.sprite = clothIcons[3];
                break;
            case 4:
                currentCloth = Clothing.Shoes;
                currentIcon.sprite = clothIcons[4];
                break;
            case 5:
                currentCloth = Clothing.Special;
                currentIcon.sprite = clothIcons[5];
                break;

            default:
                break;
        }

    }

    public void OpenCloth()
    {
        if (!ddOpened)
        {
            m_DropdownAnimation.clip = openAnim;
            m_DropdownAnimation.Play();
            ddOpened = true;
        }
        else
        {
            m_DropdownAnimation.clip = closedAnim;
            m_DropdownAnimation.Play();
            ddOpened = false;
        }

       
       
    }
}
