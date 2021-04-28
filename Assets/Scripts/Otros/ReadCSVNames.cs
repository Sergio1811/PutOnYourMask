using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSVNames : MonoBehaviour
{
    public static ReadCSVNames names;
    public string[] spanishNames;
    public string[] englishNames;
    public string[] spanishSurnames;
    public string[] englishSurnames;
    public string[] spanishCitiesNames;
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

    string ruteNameSpanishNamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishNames.csv";//Change
    string ruteNameSpanishSurnamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\SpanishSurnames.csv";//Change
    string ruteNameEnglishNamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishNames.csv";//Change
    string ruteNameEnglishSurnamesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\EnglishSurnames.csv";//Change
    string ruteSpanishCitiesPC = "C:\\Users\\cuenta\\Documents\\GitHub\\PutOnYourMask\\Assets\\Resources\\BBDD\\Cities.csv";//Change

    string ruteNameSpanishNames = "/BBDD/SpanishNames.csv";//Change
    string ruteNameSpanishSurnames = "/BBDD/SpanishSurnames.csv";//Change
    string ruteNameEnglishNames = "/BBDD/EnglishNames.csv";//Change
    string ruteNameEnglishSurnames = "/BBDD/EnglishSurnames.csv";//Change
    string ruteSpanishCities = "/BBDD/Cities.csv";//Change

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR 
        ReadCSV(ruteNameSpanishNamesPC, out spanishNames);
        ReadCSV(ruteNameSpanishSurnamesPC, out spanishSurnames);
        ReadCSV(ruteSpanishCitiesPC, out spanishCitiesNames);//todas las ciudades en realidad
        ReadCSV(ruteNameEnglishNamesPC, out englishNames);
        ReadCSV(ruteNameEnglishSurnamesPC, out englishSurnames);

#endif
        /*
#if UNITY_ANDROID
        ReadCSVAndroid(ruteNameSpanishNames, out spanishNames);
        ReadCSVAndroid(ruteNameSpanishSurnames, out spanishSurnames);
        ReadCSVAndroid(ruteSpanishCities, out spanishCitiesNames);//todas las ciudades en realidad
        ReadCSVAndroid(ruteNameEnglishNames, out englishNames);
        ReadCSVAndroid(ruteNameEnglishSurnames, out englishSurnames);

//#endif*/
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
