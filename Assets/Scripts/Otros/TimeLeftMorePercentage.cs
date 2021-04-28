using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeftMorePercentage : MonoBehaviour
{
    public TextMeshProUGUI textTimeLeft;
    public TimeSpan timeBase;

    private void Start()
    {
        timeBase = new TimeSpan(0, 10, 0);
    }

    void Update()
    {
        TimeSpan timeLeft = timeBase - TimeSpan.FromSeconds(TimeController.instance.currentSecondsInSession);
            
        textTimeLeft.text = string.Format("{0:D2}:{1:D2}", timeLeft.Minutes, timeLeft.Seconds);
    }
}
