using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpriteAnimationRunner : MonoBehaviour
{
    Dictionary<string, GameObject> sprites = new Dictionary<string, GameObject>();

    GameObject sprite;

    public void SetRunImage(string _objectName)
    {
        if (sprites.ContainsKey(_objectName))
            sprite = sprites[_objectName];
        else
        {
            sprites.Add(_objectName, GameObject.Find(_objectName));
            sprite = sprites[_objectName];
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