using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedestriansManager : MonoBehaviour
{
    [Tooltip("In Seconds")] public int miniGameTime;
    public Text secondsText;
    int currentValue=6;
    GameObject characterPrefab;
    Material maskOnMat;
    Material maskOffMat;

    void Start()
    {
        characterPrefab = Resources.Load("Prefabs/Character") as GameObject;
        maskOffMat = Resources.Load("Materials/MaskOff") as Material;
        maskOnMat = Resources.Load("Materials/MaskOn") as Material;
    }

    // Update is called once per frame
    void Update()
    {
        #region TimeControl
        int currentMinutes = (int)(miniGameTime - Time.time) / 60;
        int currentSeconds = (int)(miniGameTime - Time.time) - (currentMinutes * 60);

        if (currentSeconds >= 10)
        {
            secondsText.text = currentMinutes + " : " + currentSeconds;
        }
        else
        {
            secondsText.text = currentMinutes + " : 0" + currentSeconds;
        }

        if((Time.time/10)>=currentValue)
        {
            StartCoroutine(fadePitch(GameManager.Instance.audioManager.pitch+0.05f));
            currentValue++;
        }
        #endregion

        #region ClickOnChar
        if (InputManager.Instance.WhatAmIClicking() != null)
        {
            if (InputManager.Instance.WhatAmIClicking().CompareTag("Character"))
            {
                GameObject saveGO = InputManager.Instance.WhatAmIClicking();

                if (saveGO.GetComponent<Renderer>().material.name.Contains(maskOnMat.name))
                {
                    int random = Random.Range(0, 1);
                    switch (random)
                    {
                        case 0:
                            Debug.Log("That's a good guy");
                            break;
                        case 1:
                            Debug.Log("That's a good girl");
                            break;
                        default:
                            break;
                    }
                }
                else if (saveGO.GetComponent<Renderer>().material.name.Contains(maskOffMat.name))
                {
                    Debug.Log("Put your fucking mask on!");
                    saveGO.transform.parent.GetComponent<Animation>().Play();
                    saveGO.GetComponent<Renderer>().material = maskOnMat;
                    VSFX.instance.CreateParticleSystem(VSFX.instance.convertPS, saveGO.transform.parent.transform.position);
                    VSFX.instance.PlayAudio(VSFX.instance.convertedSound);
                        }
            }
        }
        #endregion
    }

    IEnumerator fadePitch(float next)
    {

        while (GameManager.Instance.audioManager.pitch < next)
        {
            GameManager.Instance.audioManager.pitch = Mathf.Lerp(GameManager.Instance.audioManager.pitch, next, 0.025f);

            yield return null;
        }

    }
}
