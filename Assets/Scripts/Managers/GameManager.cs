using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public GameObject[] noticias;
    GameObject panelNoticias;

    public PercentageVirusControl vsControl;

    [Tooltip("Porcentaje en decimal de la probabilidad")]
    [Range(0, 1)] public float newProbability;

    public bool onlineShopping;

    public string language;

    #region  
    public GameObject headGO;
    public GameObject maskGO;
    public GameObject shirtGO;
    public GameObject pantsGO;
    public GameObject shoeGO;

    [HideInInspector] public bool GeneratedMiniGames = false;
    #endregion Ropa
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += LoadLevel0;
            language = LocaleHelper.GetSupportedLanguageCode();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(instance);
    }


    private void LoadLevel0(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            GeneratedMiniGames = false;
            vsControl = GameObject.Find("ScriptHolder").GetComponent<PercentageVirusControl>();
            coinsText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<TextMeshProUGUI>();
            waypointsParent = GameObject.Find("WaypointsSpawnMinigames");
            panelNoticias = GameObject.Find("PanelNoticias");
            panelNoticias.SetActive(false);

            

            if (Random.value > 1 - newProbability)
            {
                NewNews();
            }
            else
            {
                StartCoroutine("NeedMinigames");
            }

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

        if (Input.GetKeyDown(KeyCode.M))
        {
            AddCoins(1000);
        }
    }

    IEnumerator NeedMinigames()
    {
        yield return new WaitForSeconds(0.2f);
        if (!GeneratedMiniGames)
        {
            InstantiateMinigames();
        }
    }
    public void InstantiateMinigames()
    {
        GeneratedMiniGames = true;
        placesToMinigame = waypointsParent.GetComponentsInChildren<Transform>();
        placesNotUsed = placesToMinigame.OfType<Transform>().ToList();
        virusPercentage = Mathf.Clamp(virusPercentage, 0, 100);
        currentMiniGamesOnMenu = (int)(virusPercentage / 10);

        if (currentMiniGamesOnMenu>10)
        {
            currentMiniGamesOnMenu = 10;
        }

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
                          VSFX.instance.PlayAudio(VSFX.instance.clicSound);
                          SceneController.instance.ChargeMiniGameAccessControl();
                      });
                    break;
                case "Lab(Clone)":
                    Button miniGameButton2 = miniGameTemp.GetComponentInChildren<Button>();
                    miniGameButton2.onClick.AddListener(
                      delegate
                      {
                          VSFX.instance.PlayAudio(VSFX.instance.clicSound);
                          SceneController.instance.ChargeMiniGameLab();
                      });
                    break;
                case "Mask(Clone)":
                    Button miniGameButton3 = miniGameTemp.GetComponentInChildren<Button>();
                    miniGameButton3.onClick.AddListener(
                      delegate
                      {
                          VSFX.instance.PlayAudio(VSFX.instance.clicSound);
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
        PlayerPrefs.SetInt("Coins", coins);
        coinsText.text = coins.ToString();
    }


    public void NewNews()
    {
        panelNoticias.SetActive(true);
        panelNoticias.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject go = Instantiate(noticias[Random.Range(0, noticias.Length)], panelNoticias.transform);
        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            virusPercentage = Mathf.Clamp(virusPercentage + 20, 0, 100);
            vsControl.PercentageUI();
            go.GetComponent<Animation>().Play();
            VSFX.instance.PlayAudio(VSFX.instance.flipPageSound);
            InstantiateMinigames();
            panelNoticias.GetComponent<Button>().onClick.AddListener(delegate
            {
                panelNoticias.SetActive(false);
                Destroy(go);
            });
        });
    }
}
