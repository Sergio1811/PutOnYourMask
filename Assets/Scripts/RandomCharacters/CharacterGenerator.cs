using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public static CharacterGenerator instance;

    [SerializeField] private Material[] cloths;
    [SerializeField] private Material[] skinColors;

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform parentTransform;

    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GenerateRandomCharacter()
    {
        RandomCharacter randomCharacter = Instantiate(characterPrefab, parentTransform).GetComponent<RandomCharacter>();
        foreach (BodyPart item in System.Enum.GetValues(typeof(BodyPart)))
        {
            randomCharacter.ChangeClothes(cloths[Random.Range(0, cloths.Length)], item);
        }
        randomCharacter.ChangeSkinsColor(skinColors[Random.Range(0, skinColors.Length)]);
    }
}
