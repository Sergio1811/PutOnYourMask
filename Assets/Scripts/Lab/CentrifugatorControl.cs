using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentrifugatorControl : MonoBehaviour
{
    public GameObject[] inCentrifugator = new GameObject[2];

    public Slider timeUI;
    public Button collectButton;
    public Image itemCollectable;

    public float timeToCentrifugate;
    float currentTimeCentrifugating;

    bool isCentrifugating = false;

    void Start()
    {
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTimeCentrifugating>=timeToCentrifugate)
        {
            FinishCentrifugate();
        }

        if(isCentrifugating)
        {
            currentTimeCentrifugating += Time.deltaTime;
            timeUI.value = currentTimeCentrifugating / timeToCentrifugate;
        }
        else 
        {
            isCentrifugating = MachineFull();

        }
    }

    public void FinishCentrifugate()
    {
        currentTimeCentrifugating = 0;
        isCentrifugating = false;
        EmptyMachine();
        timeUI.value = 0;
        timeUI.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(true);
        PopUpObject();

    }

    public bool MachineFull()
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            if (inCentrifugator[i] == null)
                return false;
        }

        timeUI.gameObject.SetActive(true);
        return true;
    }

    public void EmptyMachine()
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            Destroy(inCentrifugator[i].gameObject);
            inCentrifugator[i] = null;
        }
    }

    public void AddObject(GameObject objectToCentrifugate)
    {
        for (int i = 0; i < inCentrifugator.Length; i++)
        {
            if (inCentrifugator[i] == null)
            {
                inCentrifugator[i] = objectToCentrifugate;
                break;
            }
        }
    }

    public void PopUpObject()
    {
        collectButton.gameObject.SetActive(true);
        //       itemCollectable.sprite = ;
    }
}
