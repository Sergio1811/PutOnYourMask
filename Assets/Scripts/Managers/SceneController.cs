using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    string tetrisGameName = "TetrisPrueba";
    string accessGameName = "LoadingScenePapers";
    string masksGameName = "MascaretesAlCarrer";
    string labGameName = "LoadingSceneLab";

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(instance);
    }

    public void ChargeMiniGameTetris()
    {
        SceneManager.LoadScene(tetrisGameName);
    }

    public void ChargeMiniGameAccessControl()
    {
        SceneManager.LoadScene(accessGameName);
    }

    public void ChargeMiniGameMasksAtStreet()
    {
        SceneManager.LoadScene(masksGameName);
    }

    public void ChargeMiniGameLab()
    {
        SceneManager.LoadScene(labGameName);
    }

    public void ChargeMainMenu()
    {
        SceneManager.LoadScene(0); //o 1

    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
