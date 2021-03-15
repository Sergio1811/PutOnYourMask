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
        PlayerPrefs.SetString(this.gameObject.name, ""); //ELIMINAR EN BUILD FINAL

        if (PlayerPrefs.GetString(this.gameObject.name) == "Completed")
        {
            Destroy(this.gameObject);
        }
        

       boton.onClick.AddListener(delegate { FirstTutoTran(); });
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
