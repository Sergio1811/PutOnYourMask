using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocChanger : MonoBehaviour
{
    public Material[] materialsDoc;
    public SkinnedMeshRenderer meshDoc1;
    public SkinnedMeshRenderer meshDoc2;
    
    void Start()
    {
        ChangeCloth();
    }

   public void ChangeCloth()
    {
        int rnd = Random.Range(0, materialsDoc.Length);
        meshDoc1.material = materialsDoc[rnd];
        meshDoc2.material = materialsDoc[rnd];
    }
}
