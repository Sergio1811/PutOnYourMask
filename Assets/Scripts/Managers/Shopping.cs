using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    public int vaccPerc;
    private void Start()
    {
        if (!CheckBought())
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                // print("Done");
                Buy();
            });
        }

    }
    public void Buy()
    {
        print("Done");
        string text = "";
        if (this.transform.name != "ButtonTobuy")
        {
             text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
        else
        {
             text = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        }
        char[] tablaDeChars = text.ToCharArray();
        int value = 99;

        for (int i = 0; i < tablaDeChars.Length; i++)
        {
            if (tablaDeChars[i] == '.')
            {
                value = i;
            }
        }

        string finalText = text;
        if (value != 99)
        {

            finalText = text.Substring(0, value);
            finalText += text.Substring(value + 1, text.Length - (value + 1));

        }


        int price = int.Parse(finalText);

        if (price <= GameManager.instance.coins)
        {
            print("Comprado");
            if (this.transform.name != "ButtonTobuy")
            {
                GameObject GOBuy = MenuController.instance.PurchaseItem(MenuController.instance.catalogo);
                GOBuy.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    GameManager.instance.AddCoins(-price);
                    Destroy(GOBuy);
                    ClothManager.instance.AllCloth.Add(int.Parse(this.transform.parent.GetChild(1).GetComponent<RawImage>().texture.name));
                    ClothManager.instance.SaveData();
                    BoughtDisplayed();
                });
            }
            else
            {
                GameObject GOBuy = MenuController.instance.PurchaseItem(MenuController.instance.tiendaDivisas);
                GOBuy.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
                {
                    GameManager.instance.AddCoins(-price);
                    GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage - vaccPerc, 0, 100);
                    PlayerPrefs.SetInt("Virus", (int)GameManager.instance.virusPercentage);

                    GameManager.instance.vsControl.PercentageUI();
                    Destroy(GOBuy);                    
                });
            }
            

        }
        else
        {
            if (this.transform.name != "ButtonTobuy")
            {
                MenuController.instance.NoMoneyPurchaseItem(MenuController.instance.catalogo);
            }
            else
            {
                MenuController.instance.NoMoneyPurchaseItem(MenuController.instance.tiendaDivisas);
            }
            print("puto pobre");
            //Instantiate PopUP
        }
    }

    public bool CheckBought()
    {
        if (this.transform.name !="ButtonTobuy")
        {
            int num = int.Parse(this.transform.parent.GetChild(1).GetComponent<RawImage>().texture.name);
            foreach (var item in ClothManager.instance.AllCloth)
            {
                if (item == num)
                {
                    BoughtDisplayed();
                    print("displayed" + num);
                    return true;
                }
            }
        }


        return false;

    }

    public void BoughtDisplayed()
    {
        this.GetComponent<Button>().interactable = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }
}
