using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationLocale
{
    public const string EN = "EN";
    public const string ES = "ES";
    public const string CA = "CA";

}

public static class LocaleHelper
{
    /// <summary>
    /// Helps to convert Unity's Application.systemLanguage to a
    /// 2 letter ISO country code. English will be returned by default.
    /// Otherwise supported language code will be returned.
    /// </summary>
    /// <returns>
    /// The 2-letter ISO code from system language if the language is supported by the application.
    /// If the language is not supported English will be returned.
    /// </returns>
    public static string GetSupportedLanguageCode()
    {
        SystemLanguage lang = Application.systemLanguage;

        switch (lang)
        {
            case SystemLanguage.Spanish:
                return ApplicationLocale.ES;
            case SystemLanguage.Catalan:
                return ApplicationLocale.CA;
            case SystemLanguage.English:
                return ApplicationLocale.EN;
            default:
                return GetDefaultSupportedLanguageCode();
        }
    }

    public static string GetDefaultSupportedLanguageCode()
    {
        return ApplicationLocale.EN;
    }
}
   /*
 * Internationalization 
 * 
 * Author: Daniel Erdmann
 * 
 * 1. Add this File to you Project
 * 
 * 2. Add the language files to the folder Assets/Resources/I18n. (Filesnames: en.txt, es.txt, pt.txt, de.txt, and so on)
 *    Format: en.txt:           es.txt:
 *           =============== =================
 *           |hello=Hello  | |hello=Hola     |
 *           |world=World  | |world=Mundo    |
 *           |...          | |...            |
 *           =============== =================
 *           
 * 3. Use it! 
 *    Debug.Log(I18n.Fields["hello"] + " " + I18n.Fields["world"]); //"Hello World" or "Hola Mundo"
 * 
 * Use \n for new lines. Fallback language is "en"
 */
class LanguageManager 
{
   
    /// <summary>
    /// Text Fields
    /// Useage: Fields[key
    /// Example: I18n.Fields["world"]
    /// </summary>
    public static Dictionary<string, string> Fields { get; private set; }

    /// <summary>
    /// Init on first use
    /// </summary>
    static LanguageManager()
    {
        LoadLanguage();
    }

    /// <summary>
    /// Load language files from ressources
    /// </summary>
    private static void LoadLanguage()
    {
        if (Fields == null)
            Fields = new Dictionary<string, string>();

        Fields.Clear();

        string lang = LocaleHelper.GetSupportedLanguageCode().ToLower();
        Debug.Log(lang);
        //lang = "es";
        var textAsset = Resources.Load(@"Translation/" + lang); //no .txt needed
        string allTexts = "";
        if (textAsset == null)
            textAsset = Resources.Load(@"Translation/en") as TextAsset; //no .txt needed
        if (textAsset == null)
            Debug.LogError("File not found for Translation: Assets/Resources/Translation/" + lang + ".txt");

        allTexts = (textAsset as TextAsset).text;

        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);

        string key, value;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }

}
