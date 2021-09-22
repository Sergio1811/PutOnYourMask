using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccesCanvasControler : MonoBehaviour
{
    public static AccesCanvasControler instance;

    [HideInInspector]
    public string[] spanishNames =  { "Sofia","Lucia","Martina","Hugo","Lucas","Daniel","Pablo","Mateo","Alejandro", "Leo","Manuel","Maria","Paula","Julia","Emma","Valeria","Daniela","Alba","David","Mario","Diego"
            ,"Claudia","Marta","Marc","Albert","Sergio","Pau","Ariadna","Marina","Manuel","Carmen","Antonio","Josefa","Jose","Isabel","Ismael","Cristina","Francisco","Javier","Sara","Carla","Juan","Raquel"
            ,"Noa","Pilar","Laura","Ramon","Vicente","Teresa"};
    [HideInInspector]
    public string[] englishNames = { "Harry", "Oliver", "Jack", "Charlie", "Jacob", "Thomas", "Alfie", "Riley", "William", "James", "Joshua", "George", "Ethan", "Samuel", "Max", "Tyler", "Henry", "Joseph", "Isaac"
            , "Jake", "Alice", "Ava", "Amelia", "Charlotte", "Grace", "Ivy", "Elsie", "Emily", "Olivia", "Mia", "Lily", "Sophia", "Vanesa", "Jessica", "Megan", "Samantha", "Sarah", "Joanne", "Jennifer", "Linda" };
    [HideInInspector]
    public string[] spanishSurnames = { "Garcia","Gonzalez","Rodriguez","Fernandez","Lopez","Martinez","Sanchez","Perez","Jimenez","Ruiz","Hernandez","Diaz","Moreno","Muñoz","Alvarez","Romero","Alonso","Gutierrez"
            ,"Navarro","Torres","Ramos","Gil","Ramirez","Serrano","Blanco","Molina","Morales","Suarez","Ortega","Delgado","Castro","Ortiz","Rubio","Sanz","Iglesias","Castillo","Santos","Cano","Cruz","Guerrero","Caballero"
            ,"Calvo","Herrera","Flores","Campos","Reyes","Santana","Rojas","Pastor","Soto"};
    [HideInInspector]
    public string[] englishSurnames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Baker", "Page", "Gardener", "Tatcher", "Beckham", "Hashtings", "York", "Swift"
            , "Long", "Forest", "Bridge", "Hill", "Grey", "White", "Black", "Bristol", "Chester", "Winchester", "Archer", "Bird", "Cook", "Myers", "Potter", "Rider", "Stone", "Wayne", "Bruce", "Duff", "Logan", "Scott" };
    [HideInInspector]
    public string[] spanishCitiesNames = { "MADRID"," BARCELONA","SEVILLA","BILBAO","VALENCIA","TENERIFE","PARIS","NICE","MARSEILLE","ROME","LAZIO","NAPOLI","LONDON","MANCHSETER","CARDIFF","BERLIN","MUNICH","WARSAW",
        "HELSINKI", "MOSCOW","MASCATE","ANTANANARIVO", "CAPE TOWN", "YAMUSUKRO", "CAIRO", "YAMENA", "NAIROBI", "BOMBAY", "TOKYO", "SHANGHAI", "BANGKOK", "ASTANA", "YAKARTA", "SIDNEY", "WELLINGTON", "BUENOS AIRES","RÍO DE JANEIRO", "QUITO","CARACAS", "CIUDAD DE MÉXICO", "WASHINGTON", "NEW YORK", "LOS ÁNGELES", "MONTREAL", "REYKJAVIK", "NUUK"};
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

        print(ReadCSVNames.names.spanishNames);
        if (rand == 0)
            return spanishNames[Random.Range(0, spanishNames.Length)];
        else
            return englishNames[Random.Range(0, englishNames.Length)];

    }

    public string GetRandomSurname()
    {
        rand = Random.Range(0, 2);
        if (rand==0)
        return spanishSurnames[Random.Range(0, spanishSurnames.Length)].ToString();
        else
        return englishSurnames[Random.Range(0, englishSurnames.Length)].ToString();

    }

    public string GetRandomCity()
    {
        return spanishCitiesNames[Random.Range(0, spanishCitiesNames.Length)];
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
