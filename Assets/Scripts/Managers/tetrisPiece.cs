using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetrisPiece : MonoBehaviour
{
    float previousTime;
    public Vector3 rotationPoint;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForLines();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += TetrisGameManager.instance.vectorLeft;//move direction 

            if(OutOfBounds()) //check in bounds, if not movement back
                transform.position -= TetrisGameManager.instance.vectorLeft;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            transform.position += TetrisGameManager.instance.vectorRight;

            if (OutOfBounds())
                transform.position -= TetrisGameManager.instance.vectorRight;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)) //rotate from point
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.up, 90);

            if(OutOfBounds())
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.up, -90);

        }

        if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? TetrisGameManager.instance.fallTime / 10 : TetrisGameManager.instance.fallTime))
        {
            transform.position += TetrisGameManager.instance.vectorDown;
            previousTime = Time.time;

            if (OutOfBounds())
            {
                transform.position -= TetrisGameManager.instance.vectorDown;
                AddToGrid();
                this.enabled = false;
                TetrisGameManager.instance.NewPiece();
            }
        }
    }

    private bool OutOfBounds() //Check in-frame
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            if (roundedX < TetrisGameManager.instance.minWidth || roundedX >= TetrisGameManager.instance.maxWidth || roundedZ < TetrisGameManager.instance.minHeight || roundedZ > TetrisGameManager.instance.maxHeight)
            {
                return true;
            }

            if (TetrisGameManager.instance.grid[roundedX, roundedZ] != null)
                return true;

        }

        return false;
    }

    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            TetrisGameManager.instance.grid[roundedX, roundedZ] = children;
        }
    }

    private void CheckForLines() //manage full row
    {
        for (int i = TetrisGameManager.instance.maxHeight-1; i >=TetrisGameManager.instance.minHeight; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private bool HasLine (int i) //check if full row
    {
        for (int j = 0; j < TetrisGameManager.instance.maxWidth; j++)
        {
            if (TetrisGameManager.instance.grid[j, i] == null)
                return false;
        }

        return true;
    }

    private void DeleteLine(int i) //Delete row
    {
        for (int j= 0; j < TetrisGameManager.instance.maxWidth; j++)
        {
            Destroy(TetrisGameManager.instance.grid[j, i].gameObject); //ADD EFFECTS
            TetrisGameManager.instance.grid[j, i] = null;
        }
    }

    private void RowDown(int i) //Down all game
    {
        for (int y = 0; y < TetrisGameManager.instance.maxHeight; y++)
        {
            for (int j = 0; j < TetrisGameManager.instance.maxWidth; j++)
            {
                if(TetrisGameManager.instance.grid[j,y] !=null)
                {
                    TetrisGameManager.instance.grid[j, y - 1] = TetrisGameManager.instance.grid[j, y];
                    TetrisGameManager.instance.grid[j, y] = null;
                    TetrisGameManager.instance.grid[j, y - 1].transform.position += TetrisGameManager.instance.vectorDown;
                }
            }
        }
    }
}
