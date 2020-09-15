using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector2 ToVector2(this Vector3 _Origin)
    {
        return new Vector2(_Origin.x, _Origin.z);
    }

    public static List<GameObject> GetNearbyObjectivesFromTarget(Transform target, float sqrtDistance, List<GameObject> gameObjectsList)
    {
        List<GameObject> returnableList = new List<GameObject>();
        for (int i = 0; i < gameObjectsList.Count; i++)
        {
            if(Vector2.Distance(ToVector2(target.transform.position), ToVector2(gameObjectsList[i].transform.position)) < sqrtDistance)
            {
                returnableList.Add(gameObjectsList[i]);
            }
        }

        return returnableList;
    }
}
