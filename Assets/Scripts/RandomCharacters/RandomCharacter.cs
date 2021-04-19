using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer head;
    [SerializeField] private SkinnedMeshRenderer body;
    [SerializeField] private SkinnedMeshRenderer cap;
    [SerializeField] private SkinnedMeshRenderer legs;
    [SerializeField] private SkinnedMeshRenderer toes;

    public void ChangeClothes(Material newCloth, BodyPart bodyPart)
    {
        SkinnedMeshRenderer meshToChange;

        switch (bodyPart)
        {
            case BodyPart.BODY:
                meshToChange = body;
                break;
            case BodyPart.CAP:
                meshToChange = cap;
                break;
            case BodyPart.LEGS:
                meshToChange = legs;
                break;
            case BodyPart.TOES:
                meshToChange = toes;
                break;
            default:
                meshToChange = null;
                break;
        }

        if (meshToChange != null)
        {
            Material[] mats = meshToChange.materials;

            mats[0] = newCloth;

            meshToChange.materials = mats;
        }
            
    }

    public void ChangeSkinsColor(Material newSkinColor)
    {
        head.material = newSkinColor;

        Material[] mats = body.materials;

        mats[1] = newSkinColor;

        body.materials = mats; 
        
        Material[] mats2 = legs.materials;

        mats[1] = newSkinColor;

        legs.materials = mats;

    }
}