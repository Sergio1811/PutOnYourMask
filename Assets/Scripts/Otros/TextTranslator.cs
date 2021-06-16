﻿using UnityEngine;
using TMPro;

public class TextTranslator : MonoBehaviour
{
    public string TextId;

    // Use this for initialization
    void Start()
    {
        print("hey");
        var text = GetComponent<TextMeshProUGUI>();
        if (text != null) { 
            if (TextId == "ISOCode")
                text.text = LocaleHelper.GetSupportedLanguageCode();
            else
                text.text = LanguageManager.Fields[TextId];
            print("EH");
        }
    }

}