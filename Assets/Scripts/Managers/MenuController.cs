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

    public enum Clothing { Head, Shirt, Pants, Shoes };
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
        if (currentNum >= languageObjectArray.Length)
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

    public void SelectCloth(Sprite l_Cloth)
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

        if (l_Cloth != null)
            currentIcon.sprite = l_Cloth;
       
    }
    public void SelectCloth(int l_Cloth)
    {
        switch (l_Cloth)
        {
            case 0:
                currentCloth = Clothing.Head;
                break;

            case 1:
                currentCloth = Clothing.Shirt;
                break;

            case 2:
                currentCloth = Clothing.Pants;
                break;

            case 3:
                currentCloth = Clothing.Shoes;
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
