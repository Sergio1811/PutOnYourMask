using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Net;



public class AdManager : MonoBehaviour
{

    private string gameId = "4000079";  //id para poner ads en adroid o ios
    private string myPlacementId = "rewardedVideo"; //tipo de anuncio que quieres poner
    private bool testMode = true;

    int lastNumberAd;
    

    // Start is called before the first frame update
    void Start()
    {
       // Advertisement.Initialize(gameId, testMode);
    }


    public interface IUnityAdsListener
    {
        void OnUnityAdsReady(string placementId);
        void OnUnityAdsDidError(string message);
        void OnUnityAdsDidStart(string placementId);
      //  void OnUnityAdsDidFinish(string placementId, ShowResult showResult);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void ShowRewardedAd(int _number)
    {
        if (Advertisement.IsReady(myPlacementId))
        {
            lastNumberAd = _number;
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(myPlacementId, options);
        }
        
    }

    private void HandleShowResult(ShowResult result)
    {
        Debug.Log(result);
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
            case ShowResult.Finished:
                if (lastNumberAd==0)
                {
                    GameManager.instance.coins += 500;
                }
                if (lastNumberAd == 1)
                {
                    //PONERLO BONITO
                    LabManager.instance.canvasFinale.textCoins.text = (int.Parse(LabManager.instance.canvasFinale.coins )* 2).ToString();
                }
                if (lastNumberAd == 2)
                {
                    //PONERLO BONITO
                    PedestriansManager.instance.canvasFinale.textCoins.text = (int.Parse(LabManager.instance.canvasFinale.coins )* 2).ToString();
                } 
                if (lastNumberAd == 3)
                {
                    //PONERLO BONITO
                    AccessControlManager.instance.canvasFinale.textCoins.text = (int.Parse(LabManager.instance.canvasFinale.coins )* 2).ToString();
                }
                break;
            default:
                break;
        }
    }


    /*
    public IEnumerator getTime()
    {
        WWW www = new WWW("http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php");
        

        yield return www;
        wdatos = www.text;
        string[] words = wdatos.Split('/');
        string[] PartesFecha = words[0].Split('-');

        _currentDay = PartesFecha[1];
        
    }*/

   



}
