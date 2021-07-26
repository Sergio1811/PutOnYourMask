using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothManager : MonoBehaviour
{
    public static ClothManager instance;

    public GameObject[] hats;
    public GameObject[] shirts;
    public GameObject[] pants;
    public GameObject[] masks;
    public GameObject[] shoes;
    public GameObject[] hair;

    int currentShirt;
    int currentPants;
    int currentMasks;
    int currentShoes;
    int currentHair;

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
        DontDestroyOnLoad(this);
    }

    public void NextCloth()
    {
        switch (MenuController.instance.currentCloth)
        {
            case MenuController.Clothing.Head:

                Vector3 tempPos = currentHeadGO.transform.position;
                Vector3 tempScale = currentHeadGO.transform.localScale;
                Destroy(currentHeadGO);
                currentHair++;

                if (currentHair >= hair.Length)
                {
                    currentHair = 0;
                }
                currentHeadGO = Instantiate(hair[currentHair], parent.transform);
                Relocate(currentHeadGO, tempPos, tempScale);

                break;
            case MenuController.Clothing.Shirt:

                Vector3 tempPosS = currentShirtGO.transform.position;
                Vector3 tempScaleS = currentShirtGO.transform.localScale;
                Destroy(currentShirtGO);
                currentShirt++;

                if (currentShirt >= shirts.Length)
                {
                    currentShirt = 0;
                }
                currentShirtGO = Instantiate(shirts[currentShirt], parent.transform);
                Relocate(currentShirtGO, tempPosS, tempScaleS);

                break;

            case MenuController.Clothing.Pants:

                Vector3 tempPosP = currentPantsGO.transform.position;
                Vector3 tempScaleP = currentPantsGO.transform.localScale;
                Destroy(currentPantsGO);
                currentPants++;

                if (currentPants >= pants.Length)
                {
                    currentPants = 0;
                }
                currentPantsGO = Instantiate(pants[currentPants], parent.transform);
                Relocate(currentPantsGO, tempPosP, tempScaleP);

                break;

            case MenuController.Clothing.Shoes:

                Vector3 tempPosSh = currentShoesGO.transform.position;
                Vector3 tempScaleSh = currentShoesGO.transform.localScale;
                Destroy(currentShoesGO);
                currentShoes++;

                if (currentShoes >= shoes.Length)
                {
                    currentShoes = 0;
                }

                currentShoesGO = Instantiate(shoes[currentShoes], parent.transform);
                Relocate(currentShoesGO, tempPosSh, tempScaleSh);

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
                currentMaskGO = Instantiate(masks[currentMasks], maskParent.transform);
                Relocate(currentMaskGO, tempPosM, tempScaleM);

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
    }

    public void PrevCloth()
    {
        switch (MenuController.instance.currentCloth)
        {
            case MenuController.Clothing.Head:

                Destroy(currentHeadGO);
                currentHair--;

                if (currentHair < 0)
                {
                    currentHair = hair.Length - 1;
                }
                currentHeadGO = Instantiate(hair[currentHair], parent.transform);

                break;
            case MenuController.Clothing.Shirt:

                Destroy(currentShirtGO);
                currentShirt--;

                if (currentShirt < 0)
                {
                    currentShirt = shirts.Length - 1;
                }
                currentShirtGO = Instantiate(shirts[currentShirt], parent.transform);

                break;

            case MenuController.Clothing.Pants:

                Destroy(currentPantsGO);
                currentPants--;

                if (currentPants < 0)
                {
                    currentPants = pants.Length - 1;
                }
                currentPantsGO = Instantiate(pants[currentPants], parent.transform);

                break;

            case MenuController.Clothing.Shoes:

                Destroy(currentShoesGO);
                currentShoes--;

                if (currentShoes < 0)
                {
                    currentShoes = shoes.Length - 1;
                }

                currentShoesGO = Instantiate(shoes[currentShoes], parent.transform);

                break;

            case MenuController.Clothing.Mask:

                Destroy(currentMaskGO);
                currentMasks--;

                if (currentMasks < 0)
                {
                    currentMasks = masks.Length - 1;
                }
                currentMaskGO = Instantiate(masks[currentMasks], parent.transform);

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
    }
    public void ActivateDisfraz()
    {
        disfrazGO.SetActive(true);

        currentHeadGO.SetActive(false);
        currentMaskGO.SetActive(false);
        currentShirtGO.SetActive(false);
        currentPantsGO.SetActive(false);
        currentShoesGO.SetActive(false);
    }

    public void DeactivateDisfraz()
    {
        disfrazGO.SetActive(false);

        currentHeadGO.SetActive(true);
        currentMaskGO.SetActive(true);
        currentShirtGO.SetActive(true);
        currentPantsGO.SetActive(true);
        currentShoesGO.SetActive(true);

    }

    public void RandomCloth()
    {
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            MenuController.instance.currentCloth = MenuController.Clothing.Head;
            NextCloth();
        }
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            MenuController.instance.currentCloth = MenuController.Clothing.Mask;
            NextCloth();
        }
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            MenuController.instance.currentCloth = MenuController.Clothing.Pants;
            NextCloth();
        }
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            MenuController.instance.currentCloth = MenuController.Clothing.Shirt;
            NextCloth();
        }
        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            MenuController.instance.currentCloth = MenuController.Clothing.Shoes;
            NextCloth();
        }
    }

    public void Relocate(GameObject objectToAdapt, Vector3 pos, Vector3 scale)
    {
        objectToAdapt.transform.position = pos;
        //objectToAdapt.transform.localScale = scale;
    }
}
