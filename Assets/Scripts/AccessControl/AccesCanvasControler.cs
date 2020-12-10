using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccesCanvasControler : MonoBehaviour
{
    public static AccesCanvasControler instance;

    public GameObject crossUI;
    public GameObject tickUI;

    public Canvas canvasPC;
    public GameObject allText;
    public Text nameText;
    public Text nameCity;
    public Text listSymptoms;

    int rand;

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

   public string GetRandomName()
    {
        rand = Random.Range(0, 2);

        if(rand==0)
        return ReadCSVNames.names.spanishNames[Random.Range(0, ReadCSVNames.names.spanishNames.Length)].ToString();
        else
        return ReadCSVNames.names.englishNames[Random.Range(0, ReadCSVNames.names.englishNames.Length)].ToString();

    }

    public string GetRandomSurname()
    {

        if(rand==0)
        return ReadCSVNames.names.spanishSurnames[Random.Range(0, ReadCSVNames.names.spanishSurnames.Length)].ToString();
        else
        return ReadCSVNames.names.englishSurnames[Random.Range(0, ReadCSVNames.names.englishSurnames.Length)].ToString();

    }

    public string GetRandomCity()
    {
        return ReadCSVNames.names.spanishCitiesNames[Random.Range(0, ReadCSVNames.names.spanishCitiesNames.Length)];
    }

    public void ChangeName()
    {
        nameText.text = GetRandomName() +" " + GetRandomSurname() +", " + Random.Range(18, 90);
        ChangeCity();
    }

    public void ChangeCity()
    {
        nameCity.text = GetRandomCity();
    }

    public IEnumerator Cross()
    {
        allText.SetActive(false);
        GameObject temporal = Instantiate(crossUI, canvasPC.transform);
        yield return new WaitForSeconds(1);
        Destroy(temporal);
        allText.SetActive(true);
    }

    public IEnumerator Tick()
    {
        allText.SetActive(false);
        GameObject temporal = Instantiate(tickUI, canvasPC.transform);
        yield return new WaitForSeconds(1);
        Destroy(temporal);
        allText.SetActive(true);
    }
}
