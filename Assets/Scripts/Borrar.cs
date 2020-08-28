using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borrar : MonoBehaviour
{
    Vector3 nextPos;

    private void Start()
    {
        nextPos = this.transform.position + Vector3.right;
    }

    void Update()
    {
        /*
        if (Vector3.Distance(this.transform.position, nextPos) < 0.1f)
            StartCoroutine("Change");*/

        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + (Vector3.right*Mathf.Sin(Time.time)), 2 * Time.deltaTime);

    }
}
