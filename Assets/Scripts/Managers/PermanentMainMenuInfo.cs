using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentMainMenuInfo : MonoBehaviour
{

    public GameObject[] languageArray;
    public Animation ddAnim;
    public Image currentIcon;
    public GameObject[] cataloguePages;
    public GameObject[] catalogueVestPages;
    public GameObject tiendaDivisas;
    public GameObject catalogo;
    public Button[] clothButtons;
    public Button MaskButton;

    public void LoadAssets()
    {
        MenuController.instance.tiendaDivisas = tiendaDivisas;
        MenuController.instance.m_DropdownAnimation = ddAnim;
        MenuController.instance.catalogo = catalogo;
        MenuController.instance.languageObjectArray = languageArray;
        MenuController.instance.currentIcon = currentIcon;
        MenuController.instance.pages = cataloguePages;
        MenuController.instance.pagesVer = catalogueVestPages;

        for (int i = 0; i < clothButtons.Length; i++)
        {
            int x = i;
            clothButtons[x].onClick.AddListener(delegate
            {
                MenuController.instance.OpenCloth();
                MenuController.instance.SelectCloth(x);
            });
            clothButtons[x].gameObject.SetActive(false);
        }

        MaskButton.onClick.AddListener(
            delegate { 
                MenuController.instance.MaskChange();
            });
    }
}
