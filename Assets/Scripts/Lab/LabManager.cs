using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabManager : MonoBehaviour
{
    public enum GameState { Play, Finish };
    public GameState currentState = GameState.Play;
    public static LabManager instance;

    public GameObject substanceOne;
    public GameObject substanceTwo;
    public PlayerLabMovement player;


    public CentrifugatorControl centrifugator;
    public WarmerControl warmer;

    public RecipeDatabase recipeDB;
    public ItemDatabase itemDB;

    public GameObject itemTemplate;

    public float timeMinigame;
    public TextMeshProUGUI timeText;
    int currentValue = 6;
    float currentTimeMinigame;

    public GameObject panelChuleta;
    public GameObject canvasFinal;
    [HideInInspector]
    public PunctuationCanvas canvasFinale;

    #region Waypoints
    [Header("Waypoints")]
    public Transform mostradorPosition;
    public Transform sub1;
    public Transform sub2;
    public Transform sub3;
    public Transform trash;
    public Transform warmerPos;
    public Transform centrifugatorPos;

    #endregion

    [Header("Data")]
    public int requests;
    public int completedRequests;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        canvasFinale = canvasFinal.GetComponentInChildren<PunctuationCanvas>();
    }

    void Update()
    {
        if (currentState == GameState.Play)
        {
            ClickController();
            TimeControl();
        }
    }

    public void ClickController()
    {
        GameObject goClicked = InputManager.Instance.WhatAmIClicking();
        if (goClicked != null)
        {
            var pos = Vector3.zero;
            if (Input.touchCount > 0)
            {
                pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            switch (goClicked.tag)
            {
                case "Bin":
                    //Nothing?
                    player.nextPoint = trash.gameObject.transform.position;
                    break;

                case "LabSub1":
                    player.nextPoint = sub1.gameObject.transform.position;
                    if (AddToInventory(itemDB.GetItem(1)))
                        VSFX.instance.PlayAudio(VSFX.instance.bottleSounds[Random.Range(0, VSFX.instance.bottleSounds.Length)]);
                    break;

                case "LabSub2":
                    player.nextPoint = sub2.gameObject.transform.position;
                    if (AddToInventory(itemDB.GetItem(2)))
                        VSFX.instance.PlayAudio(VSFX.instance.bottleSounds[Random.Range(0, VSFX.instance.bottleSounds.Length)]);
                    break;

                case "LabSub3":
                    player.nextPoint = sub3.gameObject.transform.position;
                    if (AddToInventory(itemDB.GetItem(3)))
                        VSFX.instance.PlayAudio(VSFX.instance.bottleSounds[Random.Range(0, VSFX.instance.bottleSounds.Length)]);
                    break;

                case "Warmer":
                    player.nextPoint = warmerPos.position;                    
                    // GameObject Item3 = Instantiate(itemTemplate);
                    break;

                case "Centrifugator":
                    player.nextPoint = centrifugatorPos.transform.position;
                    //Instantiate(itemDB.GetItem(3).gameObject);
                    break;

                case "Character":
                    player.nextPoint = mostradorPosition.transform.position;
                    //Nothing
                    break;

                case "ObjectInventory":
                    //
                    break;

                case "Button":
                    panelChuleta.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }

    public bool AddToInventory(Item objectToAdd)
    {
        int number;
        GameObject go = InventoryLab.instance.CheckInventorySpace(out number);
        if (go != null)
        {
            GameObject Item = Instantiate(itemTemplate, go.transform);
            Item.transform.localPosition = Vector3.zero;
            Item.GetComponent<Image>().sprite = objectToAdd.icon;
            //Item.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/LiquidsAnimations/" + objectToAdd.title + "Anim") as RuntimeAnimatorController;
            return true;
        }
        return false;
    }

    public void TimeControl()
    {
        currentTimeMinigame += Time.deltaTime;
        int currentMinutes = (int)(timeMinigame - currentTimeMinigame) / 60;
        int currentSeconds = (int)(timeMinigame - currentTimeMinigame) - (currentMinutes * 60);

        if (currentSeconds >= 10)
        {
            timeText.text = "0" + currentMinutes + ":" + currentSeconds;
        }
        else
        {
            timeText.text = "0" + currentMinutes + ":0" + currentSeconds;
        }

        if ((Time.time / 10) >= currentValue)
        {
            // StartCoroutine(fadePitch(GameManager.instance.audioManager.pitch + 0.05f));
            currentValue++;
        }
        if (currentTimeMinigame > timeMinigame)
        {
            Finish();
        }
    }

    IEnumerator fadePitch(float next)
    {

        while (GameManager.instance.audioManager.pitch < next)
        {
            GameManager.instance.audioManager.pitch = Mathf.Lerp(GameManager.instance.audioManager.pitch, next, 0.025f);

            yield return null;
        }

    }
    public void Finish()
    {
        currentState = GameState.Finish;
        //calculos de puntuacion

        canvasFinale.coins = "100";
        GameManager.instance.AddCoins(100);
        canvasFinale.iniPercentage = GameManager.instance.virusPercentage.ToString();
        canvasFinale.finalPercentage = (GameManager.instance.virusPercentage - 20).ToString();
        GameManager.instance.virusPercentage -= 20;
        canvasFinal.SetActive(true);
    }
}
