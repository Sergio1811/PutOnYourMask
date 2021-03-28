using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioManager;

    public int coins;
    public TextMeshProUGUI coinsText;
    public float virusPercentage;
    int currentMiniGamesOnMenu;
    public GameObject waypointsParent;
    Transform[] placesToMinigame;
    public GameObject[] minigamesButtons;
    List<Transform> placesNotUsed;



    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        if (SceneController.instance.GetCurrentScene() == 0)
        {
            coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
            waypointsParent = GameObject.Find("WaypointsSpawnMinigames");
            InstantiateMinigames();
            AddCoins(0);
        }
    }

    private void OnLevelWasLoaded(int level)
    {      
        if (level == 0)
        {
            coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
            waypointsParent = GameObject.Find("WaypointsSpawnMinigames");
            InstantiateMinigames();
            AddCoins(0);
        }
    }
    private void Update()
    {
        /*
        currentMiniGameTime += Time.deltaTime;

        if(currentMiniGameTime>= miniGameTime)
        {
            SceneController.instance.ChargeMainMenu();
        }*/
    }

    public void InstantiateMinigames()
    {
        
        placesToMinigame = waypointsParent.GetComponentsInChildren<Transform>();
        placesNotUsed = placesToMinigame.OfType<Transform>().ToList();
        currentMiniGamesOnMenu = (int)(virusPercentage / 10);
        for (int i = 0; i < currentMiniGamesOnMenu; i++)
        {
            int rnd = Random.Range(0, placesNotUsed.Count);
            GameObject miniGameTemp = Instantiate(minigamesButtons[Random.Range(0, minigamesButtons.Length)], placesNotUsed[rnd]);
            placesNotUsed.RemoveAt(rnd);

            switch (miniGameTemp.name)
            {
                case "PP(Clone)":
                    Button miniGameButton = miniGameTemp.GetComponentInChildren<Button>();
                    miniGameButton.onClick.AddListener(
                      delegate
                      {
                          SceneController.instance.ChargeMiniGameAccessControl();
                      });
                    break;
                case "Lab(Clone)":
                    Button miniGameButton2 = miniGameTemp.GetComponentInChildren<Button>();
                    miniGameButton2.onClick.AddListener(
                      delegate
                      {
                          SceneController.instance.ChargeMiniGameLab();
                      });
                    break;
                case "Mask(Clone)":
                    Button miniGameButton3 = miniGameTemp.GetComponentInChildren<Button>();
                    miniGameButton3.onClick.AddListener(
                      delegate
                      {
                          SceneController.instance.ChargeMiniGameMasksAtStreet();
                      });
                    break;
                default:
                    break;
            }
           
        }
    }

    public void getTime()
    {
        System.DateTime time = System.DateTime.Now;
        
    }

    public void AddCoins(int l_Coins)
    {
        coins += l_Coins;
        coinsText.text = coins.ToString();
    }
   
}
