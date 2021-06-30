using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseIAPVacc : MonoBehaviour
{
    public int percentage;

    public void OnPurchaseComplete(Product product)
    {
        GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage - percentage, 0, 100);
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + "failure due to" + reason);
    }
}
