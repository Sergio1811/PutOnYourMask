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

    float miniGameTime = 120;
    float currentMiniGameTime;

    public float virusPercentage;
    int currentMiniGamesOnMenu;
    public GameObject waypointsParent;
    Transform[] placesToMinigame;
    public GameObject[] minigamesButtons;
    List<Transform> placesNotUsed;

    public TextMeshProUGUI virusPercentageText;
    public Image bckgVirusPercentage;

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
        if(SceneController.instance.GetCurrentScene()==0)
        InstantiateMinigames();
    }

    private void Update()
    {
        currentMiniGameTime += Time.deltaTime;

        if(currentMiniGameTime>= miniGameTime)
        {
            SceneController.instance.ChargeMainMenu();
        }
    }

    public void InstantiateMinigames()
    {
        PercentageUI();
        
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

    public void PercentageUI()
    {
        virusPercentageText.text = virusPercentage.ToString() + "%";
        bckgVirusPercentage.fillAmount = virusPercentage / 100; 
    }
}
