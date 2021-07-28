using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
           // print("Done");
            Buy();
        });
    }
    public void Buy()
    {
        print("Done");
        string text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
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
        if (value!=99)
        {
            finalText = text.Substring(0, value);
            finalText += text.Substring(value + 1, text.Length-2);

        }


        int price = int.Parse(finalText);

        if (price<=GameManager.instance.coins)
        {
            print("Comprado");
            GameObject GOBuy = MenuController.instance.PurchaseItem(MenuController.instance.catalogo);
            GOBuy.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.AddCoins(-price);
                Destroy(GOBuy);
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            });
            
        }
        else
        {
            MenuController.instance.NoMoneyPurchaseItem(MenuController.instance.catalogo);
            print("puto pobre");
            //Instantiate PopUP
        }
    }
}
