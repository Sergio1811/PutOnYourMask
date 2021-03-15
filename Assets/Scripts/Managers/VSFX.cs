using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSFX : MonoBehaviour
{

    //particles and sounds library
    [Header("General")]
    public static VSFX instance;

    public GameObject AudioInstance;

    [Header("Mascaretas al carrer")]
    public AudioClip convertedSound;
    public GameObject convertPS;
    public GameObject infectedPS;
    public GameObject happyPS;
    public GameObject unhappyPS;
    public GameObject badSmellPS;

    [Header("Laboratorio")]
    public GameObject SmokePuffPS;
    public GameObject FireBunPS;
    public GameObject SmokeColumnPS;
    public AudioClip[] bottleSounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        GameObject audio = Instantiate(AudioInstance, gameObject.transform.parent);
        audio.GetComponent<AudioSource>().clip = clip;
        audio.GetComponent<AudioSource>().Play();
        Destroy(audio, clip.length + 0.25f);
    }

    public GameObject CreateParticleSystem(GameObject particleSystem, Vector3 position, bool loop)
    {
        GameObject psLocal = Instantiate(particleSystem, position, particleSystem.transform.rotation);
       /* if(!loop)
        Destroy(psLocal, psLocal.GetComponent<ParticleSystem>().startLifetime);*/

        return psLocal;
    }
}
