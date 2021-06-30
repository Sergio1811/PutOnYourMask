using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public Animator animControl;
    public Button boton;

    private void Start()
    {
        boton.onClick.AddListener(delegate { FirstTutoTran(); });

        if (PlayerPrefs.GetString(this.gameObject.name) == "Completed")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if (GameObject.Find("ScriptHolder").GetComponent<AccessControlManager>() != null)
            {
                AccessControlManager.instance.currentState = AccessControlManager.GameState.Stopped;
            }
            else if (GameObject.Find("ScriptHolder").GetComponent<LabManager>() != null)
            {
                LabManager.instance.currentState = LabManager.GameState.Stopped;
            }
        }
    }

    public void FirstTutoTran()
    {
        animControl.SetTrigger("FirstTrans");
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(delegate { SecondTutoTran(); });
    }

    public void SecondTutoTran()
    {
        animControl.SetTrigger("SecondTrans");
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(delegate
        {
            PlayerPrefs.SetString(this.gameObject.name, "Completed");

            if (GameObject.Find("ScriptHolder").GetComponent<AccessControlManager>() != null)
            {
                AccessControlManager.instance.currentState = AccessControlManager.GameState.Play;
            }
            else if (GameObject.Find("ScriptHolder").GetComponent<LabManager>() != null)
            {
                LabManager.instance.currentState = LabManager.GameState.Play;
            }
            this.gameObject.SetActive(false);
            Reset();
        });
        
    }

    public void Reset()
    {
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(delegate { FirstTutoTran(); });
    }
}
