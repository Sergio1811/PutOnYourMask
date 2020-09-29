using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    string tetrisGameName = "TetrisPrueba";
    string accessGameName = "PapersPleaseScene";
    string masksGameName = "MascaretesAlCarrer";
    string labGameName = "LabScene";

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
    }

    public void ChargeMiniGameTetris()
    {
        SceneManager.LoadScene(tetrisGameName);
    }

    public void ChargeMiniGameAccessControl()
    {
        SceneManager.LoadScene(accessGameName);
    }

    public void ChargeMiniGameMasksAtstreet()
    {
        SceneManager.LoadScene(masksGameName);
    }

    public void ChargeMiniGameLab()
    {
        SceneManager.LoadScene(labGameName);
    }

    public void ChargeMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
