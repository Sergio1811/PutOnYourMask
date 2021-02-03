using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public enum ItemType
    {
        HC100,
        HC200,
        NoAds
    }

    public ItemType itemType;
    public Text priceText;
    public string defaultText;

    void Start()
    {
        defaultText = priceText.text;
    }


    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.HC100:
                IAPManager.GetInstance().Buy100HC();
                break;
            case ItemType.HC200:
                IAPManager.GetInstance().Buy200HC();
                break;
            case ItemType.NoAds:
                IAPManager.GetInstance().BuyNoAds();
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (IAPManager.GetInstance().IsInitialized())
        {
            yield return null;
        }

        string loadedPrice = "";

        switch (itemType)
        {
            case ItemType.HC100:
                loadedPrice = IAPManager.GetInstance().GetProducePriceFromStore(IAPManager.GetInstance().HC_100);
                break;
            case ItemType.HC200:
                loadedPrice = IAPManager.GetInstance().GetProducePriceFromStore(IAPManager.GetInstance().HC_200);
                break;
            case ItemType.NoAds:
                loadedPrice = IAPManager.GetInstance().GetProducePriceFromStore(IAPManager.GetInstance().NO_ADS);
                break;
            default:
                break;
        }

        priceText.text = defaultText + " " + loadedPrice;
    }
}