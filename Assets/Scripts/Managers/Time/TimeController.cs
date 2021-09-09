using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class TimeController : MonoBehaviour
{
    public static TimeController instance = null;
    string todaysDates;
    private string _currentTime;
    private string _currentDate;
    string prevDateTry;
    string prevTimeTry;

    string fecha;
    public TimeSpan hora;
    public int dia;
    public int mes;
    public int anyo;
    string[] partesFecha;
    public int horas;
    public int min;
    public int sec;
    string[] partesHora;

    public float currentSecondsInSession;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        TimeFromLastSession();
    }


    private void Update()
    {
        currentSecondsInSession += Time.deltaTime;

        if (currentSecondsInSession >= 600)
        {
            currentSecondsInSession = 0;
            GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage + 10, 0, 100);
            PlayerPrefs.SetInt("Virus", (int)GameManager.instance.virusPercentage);

            GameManager.instance.vsControl.PercentageUI();
        }

    }


    public IEnumerator getTime()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }

        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            todaysDates = www.downloadHandler.text;
            string[] words = todaysDates.Split('/');
            Debug.Log("The new date is : " + words[0]);
            Debug.Log("The new time is : " + words[1]);

            _currentDate = words[0];
            _currentTime = words[1];

            TimeBetweenSessions();
        }
    }

    public IEnumerator saveTime()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }

        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            todaysDates = www.downloadHandler.text;
            string[] words = todaysDates.Split('/');
            Debug.Log("The previous date is : " + words[0]);
            Debug.Log("The previous time is : " + words[1]);

            _currentDate = words[0];
            _currentTime = words[1];
            TimeSpan lastMinutes = TimeSpan.Parse(_currentTime) + TimeSpan.FromSeconds(currentSecondsInSession);
            _currentTime = string.Format("{0:D2}:{1:D2}:{2:D2}", lastMinutes.Hours, lastMinutes.Minutes, lastMinutes.Seconds);

            PlayerPrefs.SetString("LastDate", _currentDate);
            PlayerPrefs.SetString("LastTime", _currentTime);
        }
    }

    public void TimeFromLastSession()
    {

        prevDateTry = PlayerPrefs.GetString("LastDate");
        prevTimeTry = PlayerPrefs.GetString("LastTime");

        print(PlayerPrefs.GetString("LastDate"));
        print(prevTimeTry);

        StartCoroutine(getTime());


    }
    void TimeBetweenSessions()
    {
        print(prevDateTry + " " + prevTimeTry);
        getDateandTime(prevDateTry, prevTimeTry);
        DateTime departure = new DateTime(anyo, mes, dia, horas, min, sec);
        getDateandTime(_currentDate, _currentTime);
        DateTime arrival = new DateTime(anyo, mes, dia, horas, min, sec);
        TimeSpan travelTime = arrival - departure;

        print((int)travelTime.TotalSeconds / 600 + "%");
        GameManager.instance.virusPercentage = PlayerPrefs.GetInt("Virus");
        GameManager.instance.virusPercentage = Mathf.Clamp(GameManager.instance.virusPercentage + ((int)travelTime.TotalSeconds / 600) * 10, 0, 100);
        PlayerPrefs.SetInt("Virus", (int)GameManager.instance.virusPercentage);
        GameManager.instance.vsControl.PercentageUI();

        currentSecondsInSession = (int)travelTime.TotalSeconds % 600;
        if (!GameManager.instance.GeneratedMiniGames)
        {
            GameManager.instance.InstantiateMinigames();
        }
    }

    void getDateandTime(string date, string time)
    {
        partesFecha = date.Split('-');
        mes = int.Parse(partesFecha[0]);
        dia = int.Parse(partesFecha[1]);
        anyo = int.Parse(partesFecha[2]);

        partesHora = time.Split(':');
        horas = int.Parse(partesHora[0]);
        min = int.Parse(partesHora[1]);
        sec = int.Parse(partesHora[2]);
    }

    private void OnApplicationQuit()
    {
        StartCoroutine(saveTime());
    }

    /*
    private void OnApplicationPause(bool pause)
    {
        StartCoroutine(saveTime());
    }
    */


}
