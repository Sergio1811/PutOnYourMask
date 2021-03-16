using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarasControl : MonoBehaviour
{
    public enum Feelings{ NEUTRAL, NEUTRAL2, SAD, SAD2, GROSS, SURPRISE, ANGRY, ANGRY2};

    public Feelings currentState = Feelings.NEUTRAL;

    public Material faceMat;

    public float time;
    void Start()
    {
        StartCoroutine(face1());
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case Feelings.NEUTRAL:
                faceMat.mainTextureOffset = new Vector2(0, 0);
                break;
            case Feelings.NEUTRAL2:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0);
                break;
            case Feelings.SAD:
                faceMat.mainTextureOffset = new Vector2(0, 0.25f);
                break;
            case Feelings.SAD2:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0.25f);
                break;
            case Feelings.GROSS:
                faceMat.mainTextureOffset = new Vector2(0, 0.5f);
                break;
            case Feelings.SURPRISE:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0.5f);

                break;
            case Feelings.ANGRY:
                faceMat.mainTextureOffset = new Vector2(0, 0.75f);

                break;
            case Feelings.ANGRY2:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0.75f);
                break;
            default:
                break;
        }
    }

    public IEnumerator face1()
    {
        currentState = Feelings.NEUTRAL;
       yield return new WaitForSeconds(time);
        StartCoroutine(face2());
    }
    
    public IEnumerator face2()
    {
        currentState = Feelings.NEUTRAL2;
        yield return new WaitForSeconds(time/3);
        StartCoroutine(face1());

    }
}
