using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReadCSVNames : MonoBehaviour
{
    public static ReadCSVNames names;
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
    public string[] spanishCitiesNames= { "MADRID"," BARCELONA","SEVILLA","BILBAO","VALENCIA","TENERIFE","PARIS","NICE","MARSEILLE","ROME","LAZIO","NAPOLI","LONDON","MANCHSETER","CARDIFF","BERLIN","MUNICH","WARSAW", 
        "HELSINKI", "MOSCOW","MASCATE","ANTANANARIVO", "CAPE TOWN", "YAMUSUKRO", "CAIRO", "YAMENA", "NAIROBI", "BOMBAY", "TOKYO", "SHANGHAI", "BANGKOK", "ASTANA", "YAKARTA", "SIDNEY", "WELLINGTON", "BUENOS AIRES", 
        "RÍO DE JANEIRO", "QUITO","CARACAS", "CIUDAD DE MÉXICO", "WASHINGTON", "NEW YORK", "LOS ÁNGELES", "MONTREAL", "REYKJAVIK", "NUUK"};
    private void Awake()
    {
        if (names == null)
        {
            names = this;
        }
        else
        {
            Destroy(this);
        }
    }
    /*
    string ruteNameSpanishNamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishNames.csv";//Change
    string ruteNameSpanishSurnamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishSurnames.csv";//Change
    string ruteNameEnglishNamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishNames.csv";//Change
    string ruteNameEnglishSurnamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishSurnames.csv";//Change
    string ruteSpanishCitiesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\Cities.csv";//Change

    string ruteNameSpanishNames = "/SpanishNames.csv";//Change
    string ruteNameSpanishSurnames = "/SpanishSurnames.csv";//Change
    string ruteNameEnglishNames = "/EnglishNames.csv";//Change
    string ruteNameEnglishSurnames = "/EnglishSurnames.csv";//Change
    string ruteSpanishCities = "/Cities.csv";//Change
    */
    // Start is called before the first frame update
    void Start()
    {
        /*
#if UNITY_EDITOR 
        ReadCSV(ruteNameSpanishNamesPC, out spanishNames);
        ReadCSV(ruteNameSpanishSurnamesPC, out spanishSurnames);
        ReadCSV(ruteSpanishCitiesPC, out spanishCitiesNames);//todas las ciudades en realidad
        ReadCSV(ruteNameEnglishNamesPC, out englishNames);
        ReadCSV(ruteNameEnglishSurnamesPC, out englishSurnames);

#endif
        
       
#if UNITY_ANDROID
        ReadCSV(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/BBDD/SpanishNames.csv", out spanishNames);
         ReadCSV(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/BBDD/SpanishSurnames.csv", out spanishSurnames);
        ReadCSV(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/BBDD/Cities.csv", out spanishCitiesNames);//todas las ciudades en realidad
        ReadCSV(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/BBDD/EnglishNames.csv", out englishNames);
        ReadCSV(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/BBDD/EnglishSurnames.csv", out englishSurnames);

#endif*/
    }

    void ReadCSV(string path, out string[] data)
    {
        data = null;
        StreamReader strReader = new StreamReader(path);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }

            data = data_String.Split(';');
        }
    }

    void ReadCSVAndroid(string path, out string[] data)
    {
        data = null;
        TextAsset theTextFile = Resources.Load<TextAsset>(path);

        bool endOfFile = false;
        while (!endOfFile)
        {
            if (theTextFile != null)
            {
                endOfFile = true;
                break;
            }

            data = theTextFile.text.Split(';');
        }
    }

}
