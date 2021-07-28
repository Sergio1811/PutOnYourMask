using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothManager : MonoBehaviour
{
    public static ClothManager instance;

    public GameObject[] heads;
    public GameObject[] shirts;
    public GameObject[] pants;
    public GameObject[] masks;
    public GameObject[] shoes;

    int currentShirt;
    int currentPants;
    int currentMasks;
    int currentShoes;
    int currentHead;

    public GameObject currentHeadGO;
    public GameObject currentShirtGO;
    public GameObject currentPantsGO;
    public GameObject currentShoesGO;
    public GameObject currentMaskGO;
    public GameObject disfrazGO;

    public GameObject parent;
    public GameObject maskParent;
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
    }

    private void Start()
    {
        MenuController.instance.currentCloth = MenuController.Clothing.Mask;
        print(PlayerPrefs.GetInt("Mask"));
        ChoseCloth(PlayerPrefs.GetInt("Mask"));
        MenuController.instance.currentCloth = MenuController.Clothing.Shirt;
        ChoseCloth(PlayerPrefs.GetInt("Shirt"));
        MenuController.instance.currentCloth = MenuController.Clothing.Pants;
        ChoseCloth(PlayerPrefs.GetInt("Pants"));
        MenuController.instance.currentCloth = MenuController.Clothing.Shoes;
        ChoseCloth(PlayerPrefs.GetInt("Shoes"));
        MenuController.instance.currentCloth = MenuController.Clothing.Head;
        ChoseCloth(PlayerPrefs.GetInt("Head"));
        if (PlayerPrefs.GetInt("Pijama") == 1)
        {
            ActivateDisfraz();
        }
        else
        {
            DeactivateDisfraz();
        }


    }

    public void NextCloth()
    {
        switch (MenuController.instance.currentCloth)
        {
            case MenuController.Clothing.Head:

                Destroy(currentHeadGO);
                currentHead++;

                if (currentHead >= heads.Length)
                {
                    currentHead = 0;
                }

                GameManager.instance.headGO = heads[currentHead];
                currentHeadGO = Instantiate(heads[currentHead], parent.transform);
                Relocate(currentHeadGO);

                PlayerPrefs.SetInt("Head", currentHead);
                break;

            case MenuController.Clothing.Shirt:


                Destroy(currentShirtGO);
                currentShirt++;

                if (currentShirt >= shirts.Length)
                {
                    currentShirt = 0;
                }
                GameManager.instance.shirtGO = shirts[currentShirt];
                currentShirtGO = Instantiate(shirts[currentShirt], parent.transform);
                Relocate(currentShirtGO);

                break;

            case MenuController.Clothing.Pants:

                Destroy(currentPantsGO);
                currentPants++;

                if (currentPants >= pants.Length)
                {
                    currentPants = 0;
                }

                GameManager.instance.pantsGO = pants[currentPants];
                currentPantsGO = Instantiate(pants[currentPants], parent.transform);
                Relocate(currentPantsGO);

                break;

            case MenuController.Clothing.Shoes:


                Destroy(currentShoesGO);
                currentShoes++;

                if (currentShoes >= shoes.Length)
                {
                    currentShoes = 0;
                }

                GameManager.instance.shoeGO = shoes[currentShoes];
                currentShoesGO = Instantiate(shoes[currentShoes], parent.transform);
                Relocate(currentShoesGO);

                break;

            case MenuController.Clothing.Mask:

                Vector3 tempPosM = currentMaskGO.transform.localPosition;
                Vector3 tempScaleM = currentMaskGO.transform.localScale;
                Destroy(currentMaskGO);
                currentMasks++;

                if (currentMasks >= masks.Length)
                {
                    currentMasks = 0;
                }
                GameManager.instance.maskGO = masks[currentMasks];
                currentMaskGO = Instantiate(masks[currentMasks], maskParent.transform);
                currentMaskGO.transform.localPosition = tempPosM;
                currentMaskGO.transform.localScale = tempScaleM;

                break;

            case MenuController.Clothing.Special:
                ActivateDisfraz();
                break;

            default:
                break;
        }
        SaveCurrent();
    }

    public void PrevCloth()
    {
        switch (MenuController.instance.currentCloth)
        {
            case MenuController.Clothing.Head:

                Destroy(currentHeadGO);
                currentHead--;

                if (currentHead < 0)
                {
                    currentHead = heads.Length - 1;
                }
                currentHeadGO = Instantiate(heads[currentHead], parent.transform);
                Relocate(currentHeadGO);
                break;
            case MenuController.Clothing.Shirt:

                Destroy(currentShirtGO);
                currentShirt--;

                if (currentShirt < 0)
                {
                    currentShirt = shirts.Length - 1;
                }
                currentShirtGO = Instantiate(shirts[currentShirt], parent.transform);
                Relocate(currentShirtGO);
                break;

            case MenuController.Clothing.Pants:

                Destroy(currentPantsGO);
                currentPants--;

                if (currentPants < 0)
                {
                    currentPants = pants.Length - 1;
                }
                currentPantsGO = Instantiate(pants[currentPants], parent.transform);
                Relocate(currentPantsGO);
                break;

            case MenuController.Clothing.Shoes:

                Destroy(currentShoesGO);
                currentShoes--;

                if (currentShoes < 0)
                {
                    currentShoes = shoes.Length - 1;
                }

                currentShoesGO = Instantiate(shoes[currentShoes], parent.transform);
                Relocate(currentShoesGO);

                break;

            case MenuController.Clothing.Mask:

                Vector3 tempPosM = currentMaskGO.transform.localPosition;
                Vector3 tempScaleM = currentMaskGO.transform.localScale;
                Destroy(currentMaskGO);
                currentMasks--;

                if (currentMasks < 0)
                {
                    currentMasks = masks.Length - 1;
                }
                currentMaskGO = Instantiate(masks[currentMasks], maskParent.transform);
                currentMaskGO.transform.localPosition = tempPosM;
                currentMaskGO.transform.localScale = tempScaleM;
                break;

            case MenuController.Clothing.Special:

                ActivateDisfraz();
                break;

            default:
                break;
        }
        SaveCurrent();
    }
    public void ActivateDisfraz()
    {
        PlayerPrefs.SetInt("Pijama", 1);
        disfrazGO.SetActive(true);

        currentHeadGO.SetActive(false);
        currentMaskGO.SetActive(false);
        currentShirtGO.SetActive(false);
        currentPantsGO.SetActive(false);
        currentShoesGO.SetActive(false);
    }

    public void DeactivateDisfraz()
    {
        PlayerPrefs.SetInt("Pijama", 0);
        disfrazGO.SetActive(false);

        currentHeadGO.SetActive(true);
        currentMaskGO.SetActive(true);
        currentShirtGO.SetActive(true);
        currentPantsGO.SetActive(true);
        currentShoesGO.SetActive(true);

    }

    public void RandomCloth()
    {
        MenuController.instance.currentCloth = MenuController.Clothing.Head;
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            NextCloth();
        }

        MenuController.instance.currentCloth = MenuController.Clothing.Mask;
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            NextCloth();
        }

        MenuController.instance.currentCloth = MenuController.Clothing.Pants;
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            NextCloth();
        }

        MenuController.instance.currentCloth = MenuController.Clothing.Shirt;
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            NextCloth();
        }

        MenuController.instance.currentCloth = MenuController.Clothing.Shoes;
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            NextCloth();
        }
        SaveCurrent();
    }

    public void Relocate(GameObject objectToAdapt)
    {
        objectToAdapt.transform.localScale *= 1.5f;
        objectToAdapt.transform.localPosition -= new Vector3(0, 0.12f, 0);

    }
    public void ChoseCloth(int clothNum)
    {
        switch (MenuController.instance.currentCloth)
        {
            case MenuController.Clothing.Head:

                Destroy(currentHeadGO);

                GameManager.instance.headGO = heads[clothNum];
                currentHeadGO = Instantiate(heads[clothNum], parent.transform);
                Relocate(currentHeadGO);

                break;
            case MenuController.Clothing.Shirt:


                Destroy(currentShirtGO);

                GameManager.instance.shirtGO = shirts[clothNum];
                currentShirtGO = Instantiate(shirts[clothNum], parent.transform);
                Relocate(currentShirtGO);

                break;

            case MenuController.Clothing.Pants:

                Destroy(currentPantsGO);

                GameManager.instance.pantsGO = pants[clothNum];
                currentPantsGO = Instantiate(pants[clothNum], parent.transform);
                Relocate(currentPantsGO);

                break;

            case MenuController.Clothing.Shoes:

                Destroy(currentShoesGO);

                GameManager.instance.shoeGO = shoes[clothNum];
                currentShoesGO = Instantiate(shoes[clothNum], parent.transform);
                Relocate(currentShoesGO);

                break;

            case MenuController.Clothing.Mask:

                Vector3 tempPosM = currentMaskGO.transform.localPosition;
                Vector3 tempScaleM = currentMaskGO.transform.localScale;
                Destroy(currentMaskGO);

                GameManager.instance.maskGO = masks[clothNum];
                currentMaskGO = Instantiate(masks[clothNum], maskParent.transform);
                currentMaskGO.transform.localPosition = tempPosM;
                currentMaskGO.transform.localScale = tempScaleM;

                break;

            case MenuController.Clothing.Special:
                disfrazGO.SetActive(true);

                currentHeadGO.SetActive(false);
                currentMaskGO.SetActive(false);
                currentShirtGO.SetActive(false);
                currentPantsGO.SetActive(false);
                currentShoesGO.SetActive(false);
                break;

            default:
                break;
        }
        SaveCurrent();
    }

    public void SaveCurrent()
    {
        PlayerPrefs.SetInt("Head", currentHead);
        PlayerPrefs.SetInt("Mask", currentMasks);
        PlayerPrefs.SetInt("Shirt", currentShirt);
        PlayerPrefs.SetInt("Pants", currentPants);
        PlayerPrefs.SetInt("Shoes", currentShoes);
    }
}
