using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PunctuationCanvas : MonoBehaviour
{
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textInitialPercentage;
    public TextMeshProUGUI textFinalPercentage;

    public string coins;
    public string iniPercentage;
    public string finalPercentage;

    public GameObject cross;
    public GameObject flecha;
    public GameObject coinVFX;


    public float pause;

    void Start()
    {
        StartCoroutine(FinishCanvasPunctuation());
    }

    IEnumerator FinishCanvasPunctuation()
    {
        // Iterate over each letter Animacion textCoins
        foreach (char letter in coins.ToCharArray())
        {
            textCoins.text += letter; // Add a single character to the GUI text
            yield return new WaitForSeconds(pause);
        }

        coinVFX.SetActive(true);
        VSFX.instance.PlayAudio(VSFX.instance.monedasSound); //Audio coins
        yield return new WaitForSeconds(1);//1 sec de espera

        //anim texto percentage initial
        foreach (char letter in iniPercentage.ToCharArray()) 
        {
            textInitialPercentage.text += letter; // Add a single character to the GUI text
            yield return new WaitForSeconds(pause);
        }
        textInitialPercentage.text += "%";//add % al final


        cross.SetActive(true);//Activar objeto cruz que tiene anim
        VSFX.instance.PlayAudio(VSFX.instance.tacharSound); // audio para el tachar
        yield return new WaitForSeconds(1);

        //miro la resta entre ini y final para cambiar a color rojo o verde
        if (int.Parse(iniPercentage) - int.Parse(finalPercentage) < 0)
        {          
            textFinalPercentage.color = new Color32(171, 14, 28, 255);
        }
        else if (int.Parse(iniPercentage) - int.Parse(finalPercentage) > 0)
        {
            textFinalPercentage.color = new Color32(0, 85, 0, 255);
        }

        //anim text final
        foreach (char letter in finalPercentage.ToCharArray())
        {
            textFinalPercentage.text += letter; // Add a single character to the GUI text
            yield return new WaitForSeconds(pause);
        }
        textFinalPercentage.text += "%";

        //miro la resta entre ini y final para activar y/o rotar la flecha

        if (int.Parse(iniPercentage) - int.Parse(finalPercentage) < 0)
        {
            flecha.SetActive(true);
            flecha.GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, 180);
        }
        else if (int.Parse(iniPercentage) - int.Parse(finalPercentage) > 0)
        {
            flecha.SetActive(true);
        }



        yield return null;
    }
}
