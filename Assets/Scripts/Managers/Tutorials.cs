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
    }

    public void FirstTutoTran( )
    {
        animControl.SetTrigger("FirstTrans");
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(delegate { SecondTutoTran(); });
    }
    public void SecondTutoTran()
    {
        animControl.SetTrigger("SecondTrans");
        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(delegate { Destroy(this.gameObject); });
        PlayerPrefs.SetString(this.gameObject.name, "Completed");
    }
}
