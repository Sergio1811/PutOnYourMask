using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PurchaseIAPVacc : MonoBehaviour
{
    public int percentage;

    public void OnPurchaseComplete(Product product)
    {
        GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage - percentage, 0, 100);
        GameManager.instance.vsControl.PercentageUI();
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + "failure due to" + reason);
    }

    public void BuyVaccMoney(int price)
    {
        if (price <= GameManager.instance.coins)
        {
            print("Comprado");
            GameObject GOBuy = MenuController.instance.PurchaseItem(MenuController.instance.catalogo);
            GOBuy.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate {
                GameManager.instance.AddCoins(-price);
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
