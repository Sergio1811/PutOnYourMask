using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillIcon : MonoBehaviour
{
    public Slider slider;
    public Image virusSatur;
    

    // Update is called once per frame
    void Update()
    {
        virusSatur.fillAmount = slider.value;
    }
}
