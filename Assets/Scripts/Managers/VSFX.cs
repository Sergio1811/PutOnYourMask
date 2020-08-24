using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSFX : MonoBehaviour
{

    //particles and sounds library
    public static VSFX instance;

    public GameObject AudioInstance;
    public AudioClip convertedSound;
    public GameObject convertPS;

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

    public void CreateParticleSystem(GameObject particleSystem, Vector3 position)
    {

        GameObject psLocal = Instantiate(particleSystem, position, particleSystem.transform.rotation);
        Destroy(psLocal, psLocal.GetComponent<ParticleSystem>().startLifetime);

    }
}
