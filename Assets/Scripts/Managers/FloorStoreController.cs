using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorStoreController : MonoBehaviour
{
    public GameObject[] closedStore;
    public Button[] storeButtons;
    public Sprite onlineIcon;
    public Sprite compraIcon;
    public Image filledBar;
    public GameObject divisasTienda;
    public GameObject catalogo;

    int virus;
    int currentFloor;

    void Start()
    {
        InitializeStore();
    }

    public void InitializeStore()
    {
        virus = (int)GameManager.instance.virusPercentage;
        filledBar.fillAmount = GameManager.instance.virusPercentage / 100;

        CurrentFloor();

        foreach (var item in closedStore)
        {
            item.SetActive(false);
        }

        foreach (var item in storeButtons)
        {
            item.GetComponent<Image>().sprite = compraIcon;
            item.onClick.RemoveAllListeners();
            item.onClick.AddListener(delegate
            {
                catalogo.SetActive(true);

            });
        }

        for (int i = 0; i < storeButtons.Length; i++)
        {
            switch (i)
            {
                case 0:
                storeButtons[0].onClick.RemoveAllListeners();
                storeButtons[0].onClick.AddListener(delegate { divisasTienda.SetActive(true); });
                    break;
                case 1:
                    storeButtons[1].onClick.AddListener(delegate { catalogo.GetComponent<CatalogueController>().PostItClickedObject(1); });
                    break;
                case 2:
                    storeButtons[2].onClick.AddListener(delegate { catalogo.GetComponent<CatalogueController>().PostItClickedObject(2); });
                    break;
                case 3:
                    storeButtons[3].onClick.AddListener(delegate { catalogo.GetComponent<CatalogueController>().PostItClickedObject(0); });
                    break;
                case 4:
                    storeButtons[4].onClick.AddListener(delegate { catalogo.GetComponent<CatalogueController>().PostItClickedObject(5); });
                    break;
                default:
                    break;
            }
          
        }

        for (int i = currentFloor + 1; i < closedStore.Length; i++)
        {
            closedStore[i].SetActive(true);
            storeButtons[i].GetComponent<Image>().sprite = onlineIcon;
            storeButtons[i].GetComponent<Button>().onClick.AddListener(delegate
            {
                GameManager.instance.onlineShopping = true;
            });
        }

    }

    public void CurrentFloor()
    {
        if (virus < 10)
        {
            currentFloor = 5;
        }
        else if (virus < 30)
        {
            currentFloor = 4;
        }
        else if (virus < 50)
        {
            currentFloor = 3;
        }
        else if (virus < 70)
        {
            currentFloor = 2;
        }
        else if (virus < 90)
        {
            currentFloor = 1;
        }
        else
        {
            currentFloor = 0;
        }
    }
}
