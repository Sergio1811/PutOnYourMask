using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSVNames : MonoBehaviour
{
    public static ReadCSVNames names;
    [HideInInspector]public string[] spanishNames;
    [HideInInspector]public string[] englishNames;
    [HideInInspector]public string[] spanishSurnames;
    [HideInInspector]public string[] englishSurnames;
    [HideInInspector]public string[] spanishCitiesNames;
    [HideInInspector]public string[] englishCitiesNames;
    private void Awake()
    {
        if (names==null)
        {
            names = this;
        }
        else
        {
            Destroy(this);
        }
    }

    string ruteNameSpanishNames = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishNames.csv";//Change
    string ruteNameSpanishSurnames = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishSurnames.csv";//Change
    string ruteNameEnglishNames = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishNames.csv";//Change
    string ruteNameEnglishSurnames = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishSurnames.csv";//Change
    string ruteSpanishCities = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\Cities.csv";//Change
    string ruteEnglishCities = "C:\\Users\\sergi\\OneDrive\\Documentos\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishCities.csv";//Change
    
    // Start is called before the first frame update
    void Start()
    {
        ReadSpanishNames();
        ReadSpanishSurnames();
        ReadEnglishNames();
        ReadEnglishSurnames();
        ReadSpanishCityNames();
    }

    void ReadSpanishNames()
    {
        StreamReader strReader = new StreamReader(ruteNameSpanishNames);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            spanishNames = data_String.Split(';');
        }

    }

    void ReadSpanishSurnames()
    {
        StreamReader strReader = new StreamReader(ruteNameSpanishSurnames);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            spanishSurnames = data_String.Split(';');
        }

    }

    void ReadEnglishNames()
    {
        StreamReader strReader = new StreamReader(ruteNameEnglishNames);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            englishNames = data_String.Split(';');
        }

    }

    void ReadEnglishSurnames()
    {
        StreamReader strReader = new StreamReader(ruteNameEnglishSurnames);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            englishSurnames = data_String.Split(';');
        }

    }

    void ReadSpanishCityNames()
    {
        StreamReader strReader = new StreamReader(ruteSpanishCities);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            spanishCitiesNames = data_String.Split(';');
        }
    }

    void ReadEnglishCityNames()
    {
        StreamReader strReader = new StreamReader(ruteEnglishCities);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String==null)
            {
                endOfFile = true;
                break;
            }

            englishCitiesNames = data_String.Split(';');
        }
    }

}
