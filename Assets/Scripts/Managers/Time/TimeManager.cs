using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager sharedInstance = null;
    string todaysDates;
    private string _currentTime;
    private string _currentDate;



    public void OnClick()
    {
        StartCoroutine(getTime());
    }

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        StartCoroutine("getTime");
    }


    public IEnumerator getTime()
    {
        WWW www = new WWW("http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php");

        yield return www;
        todaysDates = www.text;
        string[] words = todaysDates.Split('/');
        // Debug.Log("The date is : " + words[0]);
        //  Debug.Log("The time is : " + words[1]);

        _currentDate = words[0];
        _currentTime = words[1];

    }

    public string getCurrentDateNow()
    {
        return _currentDate;
    }

    public string getCurrentTimeNow()
    {
        return _currentTime;
    }

    private void Done()
    {
        print(todaysDates);
    }

}