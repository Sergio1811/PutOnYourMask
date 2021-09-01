using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOff : MonoBehaviour
{
    AudioSource music;

    private void Start()
    {
        music = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        music.mute = !MenuController.instance.musicOn;
    }
}
