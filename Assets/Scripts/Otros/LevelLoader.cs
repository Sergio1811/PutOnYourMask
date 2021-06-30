﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI progressText;

    private void Start()
    {
        StartCoroutine(WaitForLoadingBar(2));
    }


    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        
        while (!operation.isDone)
        {
            /*float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100 + "%";*/
            yield return null;
        }

    }
    IEnumerator WaitForLoadingBar(float seconds)
    {
        float currentTime = seconds;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        StartCoroutine(LoadAsynchronously());

    }
}
