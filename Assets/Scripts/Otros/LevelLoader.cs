using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI progressText;
    public int scene;

    private void Start()
    {
        StartCoroutine(WaitForLoadingBar(2));
    }


    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = Mathf.Round(progress) * 100 + "%";
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
