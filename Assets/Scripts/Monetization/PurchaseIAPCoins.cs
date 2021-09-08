using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseIAPCoins : MonoBehaviour
{
    public int coins;

    public void OnPurchaseComplete(Product product)
    {
        GameManager.instance.AddCoins(coins);

    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + "failure due to" + reason);
    }
}
