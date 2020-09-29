using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]


public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    int amountToPool;


    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        amountToPool = pooledObjects.Count;
        for (int i = 0; i < amountToPool; i++)
        {           
            pooledObjects[i].SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
        return null;
    }

   
}
