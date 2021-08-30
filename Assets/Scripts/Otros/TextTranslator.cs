using UnityEngine;
using TMPro;

public class TextTranslator : MonoBehaviour
{
    public string TextId;

    // Use this for initialization
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (text != null) { 
            if (TextId == "ISOCode")
                text.text = GameManager.instance.language;
            else
                text.text = LanguageManager.Fields[TextId];
        }
    }

}