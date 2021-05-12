using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString(this.gameObject.name, "NotCompleted");
        if (PlayerPrefs.GetString(this.gameObject.name) == "Completed")
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
