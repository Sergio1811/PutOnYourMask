using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    private void Start()
    {
        GameObject GO = Instantiate(TetrisGameManager.instance.objectsInBlocks[Random.Range(0, TetrisGameManager.instance.objectsInBlocks.Length)], this.transform);
    }

    void Update()
    {
        if(Vector3.Dot(this.transform.forward, Camera.main.transform.up)!=1)
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.up);
    }

}
