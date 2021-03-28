using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PercentageVirusControl : MonoBehaviour
{
    public TextMeshProUGUI virusPercentageText;
    public Image bckgVirusPercentage;

    private void Start()
    {
        PercentageUI();
    }
    public void PercentageUI()
    {
        virusPercentageText.text = GameManager.instance.virusPercentage.ToString() + "%";
        bckgVirusPercentage.fillAmount = GameManager.instance.virusPercentage / 100;
    }
}
