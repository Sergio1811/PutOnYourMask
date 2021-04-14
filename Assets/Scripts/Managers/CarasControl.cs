using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarasControl : MonoBehaviour
{
    public enum Feelings { NEUTRAL, NEUTRAL2, SAD, SAD2, GROSS, SURPRISE, ANGRY, ANGRY2 };

    public Feelings currentState = Feelings.NEUTRAL;

    public Material faceMat;

    public float time;
    void Start()
    {
        StartCoroutine(NeutralFace());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeFace(Feelings.NEUTRAL);
            FaceAnim("NeutralFace");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeFace(Feelings.ANGRY);
            FaceAnim("AngryFace");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StopAllCoroutines();

            ChangeFace(Feelings.GROSS);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StopAllCoroutines();
            ChangeFace(Feelings.SURPRISE);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeFace(Feelings.SAD);
            FaceAnim("SadFace");
        }

    }
    // Update is called once per frame
    void ChangeFace(Feelings state)
    {
        currentState = state;
        switch (currentState)
        {
            case Feelings.NEUTRAL:
                faceMat.mainTextureOffset = new Vector2(0, 0);
                break;

            case Feelings.NEUTRAL2:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0);
                break;

            case Feelings.ANGRY:
                faceMat.mainTextureOffset = new Vector2(0, 0.263f);
                break;

            case Feelings.ANGRY2:
                faceMat.mainTextureOffset = new Vector2(0.523f, 0.255f);
                break;

            case Feelings.GROSS:
                faceMat.mainTextureOffset = new Vector2(0, 0.51f);
                break;

            case Feelings.SURPRISE:
                faceMat.mainTextureOffset = new Vector2(0.52f, 0.51f);
                break;

            case Feelings.SAD:
                faceMat.mainTextureOffset = new Vector2(0, 0.75f);
                break;

            case Feelings.SAD2:
                faceMat.mainTextureOffset = new Vector2(0.517f, 0.75f);
                break;

            default:
                break;
        }
    }

    public void FaceAnim(string corroutine)
    {
        StopAllCoroutines();
        StartCoroutine(corroutine);
    }


    public IEnumerator NeutralFace()
    {
        ChangeFace(Feelings.NEUTRAL);
        yield return new WaitForSeconds(time);
        StartCoroutine(NeutralFaceBlink());
    }

    public IEnumerator NeutralFaceBlink()
    {
        ChangeFace(Feelings.NEUTRAL2);
        yield return new WaitForSeconds(time / 5);
        StartCoroutine(NeutralFace());
    }

    public IEnumerator SadFace()
    {
        ChangeFace(Feelings.SAD);
        yield return new WaitForSeconds(time);
        StartCoroutine(SadFaceBlink());
    }

    public IEnumerator SadFaceBlink()
    {
        ChangeFace(Feelings.SAD2);
        yield return new WaitForSeconds(time / 5);
        StartCoroutine(SadFace());
    }

    public IEnumerator AngryFace()
    {
        ChangeFace(Feelings.ANGRY);
        yield return new WaitForSeconds(time);
        StartCoroutine(AngryFaceBlink());
    }

    public IEnumerator AngryFaceBlink()
    {
        ChangeFace(Feelings.ANGRY2);
        yield return new WaitForSeconds(time / 5);
        StartCoroutine(AngryFace());
    }
}
