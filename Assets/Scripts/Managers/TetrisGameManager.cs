using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    public static TetrisGameManager instance;

    public float fallTime;

    public int minHeight;
    public int maxHeight;
    public int minWidth;
    public int maxWidth;


    public Vector3 vectorLeft;
    public Vector3 vectorRight;
    public Vector3 vectorDown;

    public GameObject[] pieces;
    public Transform spawnPosition;

    [HideInInspector] public Transform[,] grid;

    public GameObject[] objectsInBlocks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        grid = new Transform[maxWidth, maxHeight];

        NewPiece();
    }

    public void NewPiece()
    {
        Instantiate(pieces[Random.Range(0, pieces.Length)], spawnPosition.position, Quaternion.identity);
    }
    
}
