using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpriteAnimationRunner : MonoBehaviour
{
    Dictionary<string, GameObject> sprites = new Dictionary<string, GameObject>();

    GameObject sprite;

    public void SetRunImage(string objectName)
    {
        if (sprites.ContainsKey(objectName))
            sprite = sprites[objectName];
        else
        {
            sprites.Add(objectName, GameObject.Find(objectName));
            sprite = sprites[objectName];
        }
    }

    public void RunImage()
    {
        sprite.GetComponent<UISpriteAnimation>().SpriteRun();
    }

    public void StopImage()
    {
        sprite.GetComponent<UISpriteAnimation>().SpriteStop();
    }

    public void OnImage()
    {
        sprite.GetComponent<UISpriteAnimation>().SpriteOn();
    }

    public void OffImage()
    {
        sprite.GetComponent<UISpriteAnimation>().SpriteOff();
    }
}