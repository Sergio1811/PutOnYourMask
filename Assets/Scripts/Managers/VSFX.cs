using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSFX : MonoBehaviour
{

    //particles and sounds library
    [Header("General")]
    public static VSFX instance;

    public GameObject AudioInstance;

    public AudioClip monedasSound;
    public AudioClip tacharSound;
    public AudioClip clicSound;
    public AudioClip backSound;
    public AudioClip flipPageSound;
    public AudioClip changeClothSound;
    public AudioClip storeOpenSound;
    public AudioClip infectionUpSound;


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
    public GameObject explosionMachinePS;
    public GameObject finishedPS;
    public AudioClip[] bottleSounds;
    public AudioClip useWarmerSound;
    public AudioClip useCentrifugatorSound;
    public AudioClip centrifugatorUsageSound;
    public AudioClip warmerUsageSound;
    public AudioClip completedOrderSound;
    public AudioClip failedOrderSound;
    public AudioClip objectToTrashSound;
    public AudioClip explosionMachineSound;
    public AudioClip popUpSound;


    [Header("AccesControl")]
    public AudioClip inaudibleSound;
    public AudioClip greenButtonSound;
    public AudioClip redButtonSound;
    public AudioClip correctSound;
    public AudioClip failSound;
    public AudioClip pickMaskSound;
    public AudioClip putMaskSound;
    public AudioClip leverSound;
    public AudioClip thermometerSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(instance);
    }

    public void PlayAudio(AudioClip clip)
    {
        if (MenuController.instance.soundOn)
        {
            GameObject audio = Instantiate(AudioInstance, gameObject.transform.parent);
            audio.GetComponent<AudioSource>().clip = clip;
            audio.GetComponent<AudioSource>().Play();
            Destroy(audio, clip.length + 0.25f);
        }
    }

    public GameObject CreateParticleSystem(GameObject particleSystem, Vector3 position, bool loop)
    {
        GameObject psLocal = Instantiate(particleSystem, position, particleSystem.transform.rotation);
        /* if(!loop)
         Destroy(psLocal, psLocal.GetComponent<ParticleSystem>().startLifetime);*/

        return psLocal;
    }
}
