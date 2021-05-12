using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimHand : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed;
    float currentSpeed;
    int currentImage;
    public Image image;
    // Update is called once per frame
    void Update()
    {
        currentSpeed += Time.deltaTime;

        if (currentSpeed>=speed)
        {
            currentSpeed = 0;
            currentImage++;

            if (currentImage >= sprites.Length)
                currentImage = 0;

            image.sprite = sprites[currentImage];
        }
    }
}
