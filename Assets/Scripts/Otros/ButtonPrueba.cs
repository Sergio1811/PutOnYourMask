using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrueba : MonoBehaviour
{
    public void PlayAnim(Animation anim)
    {
        anim.Play();
    }

    public void PlayAnimationDerecha(Animation anim)
    {
        anim.clip = anim.GetClip("SlideEnBarra");
        anim.Play();
    }
    public void PlayAnimationIzquierda(Animation anim)
    {
        anim.clip = anim.GetClip("SlideEnBarraInverso");
        anim.Play();
    }
}
