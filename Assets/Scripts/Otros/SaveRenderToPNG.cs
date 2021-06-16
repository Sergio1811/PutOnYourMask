using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveRenderToPNG))]
public class SaveImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SaveRenderToPNG renderToPNG = (SaveRenderToPNG)target;
        if (GUILayout.Button("Save"))
        {
            renderToPNG.Save();
        }
        DrawDefaultInspector();
    }
    
}
public class SaveRenderToPNG : MonoBehaviour
{
   public int imageCount;
   public Material _materialToSave;

    public Camera _cameraWithRendTexture;

     public int _heightInPixelsOfCertificate;

    public void Save()
    {

        RenderTexture.active = _cameraWithRendTexture.targetTexture;
        Texture2D newTexture = new Texture2D(_cameraWithRendTexture.targetTexture.width, _cameraWithRendTexture.targetTexture.height, TextureFormat.ARGB32, false);
        newTexture.ReadPixels(new Rect(0, 0, _cameraWithRendTexture.targetTexture.width, _cameraWithRendTexture.targetTexture.height), 0, 0, false);
        newTexture.Apply();
        Color[] colorArrayCropped = newTexture.GetPixels(0, newTexture.height - _heightInPixelsOfCertificate, newTexture.width, _heightInPixelsOfCertificate);
        Texture2D croppedTex = new Texture2D(newTexture.width, _heightInPixelsOfCertificate, TextureFormat.ARGB32, false);
        croppedTex.SetPixels(colorArrayCropped);
        croppedTex = FillInClear(croppedTex, _cameraWithRendTexture.backgroundColor);
        croppedTex.Apply();
        byte[] bytes = croppedTex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/UI/CatalogueUI/IconRender/" + imageCount + ".png", bytes);
        print("saved in: " + Application.dataPath + "/Resources/UI/CatalogueUI/IconRender/" + imageCount + ".png");
        imageCount++;
        
    }

    public Texture2D FillInClear(Texture2D tex2D, Color whatToFillWith)
    {
        for (int i = 0; i < tex2D.width; i++)
        {
            for (int j = 0; j < tex2D.height; j++)
            {
                if (tex2D.GetPixel(i, j) == Color.clear)
                    tex2D.SetPixel(i, j, whatToFillWith);
            }
        }
        return tex2D;
    }
}
