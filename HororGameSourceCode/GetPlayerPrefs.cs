using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPrefs : MonoBehaviour
{
    enum FullScreen { On = 1, Off }

    string soundKey;
    public float sound;

    string mouseKey;
    public float mouse;

    string[] screenKey;
    public int[] screen;

    string fullintKey;
    public int fullint;

    public bool fullBool;

    private void Awake()
    {
        screenKey = new string[2];
        screen = new int[2];

        soundKey = "Sound";
        mouseKey = "Mouse";
        screenKey[0] = "ScreenX";
        screenKey[1] = "ScreenY";
        fullintKey = "Full";

        GetSetting();

        if (sound == 0f)
        {
            sound = 100f;
        }
        if (mouse == 0f)
        {
            mouse = 10f;
        }
        if (screen[0] == 0)
        {
            screen[0] = 1920;
        }
        if (screen[1] == 0)
        {
            screen[1] = 1080;
        }
        if (fullint == 0)
        {
            fullint = (int)FullScreen.On;
        }
        
        if(fullint == (int)FullScreen.On)
        {
            fullBool = true;
        }
        else if(fullint == (int)FullScreen.Off)
        {
            fullBool = false;
        }

        SetSetting();
    }

    void GetSetting()
    {
        sound = PlayerPrefs.GetFloat(soundKey);
        mouse = PlayerPrefs.GetFloat(mouseKey);
        screen[0] = PlayerPrefs.GetInt(screenKey[0]);
        screen[1] = PlayerPrefs.GetInt(screenKey[1]);
        fullint = PlayerPrefs.GetInt(fullintKey);
    }

    public void SetSetting()
    {
        PlayerPrefs.SetFloat(soundKey, sound);
        PlayerPrefs.SetFloat(mouseKey, mouse);
        PlayerPrefs.SetInt(screenKey[0], screen[0]);
        PlayerPrefs.SetInt(screenKey[1], screen[1]);
        PlayerPrefs.SetInt(fullintKey, fullint);
    }
}
