using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PublicEnums;

public class TitleFunc : MonoBehaviour
{
    MapLoader mapLoader;
    
    Action myAction;

    [SerializeField]
    GameObject[] arrowGroup;

    GameObject optionFrame;

    SpriteAnimationRunner spriteRunner;

    enum _EMainSelect_
    {
        emsStart,
        emsOption,
        emsQuit,
        emsMax
    } _EMainSelect_ currentSelect;

    private void Start()
    {
        spriteRunner = GameObject.Find("UI").GetComponent<SpriteAnimationRunner>();
        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();
        spriteRunner.SetRunImage("MainWallpaper_Image");
        spriteRunner.RunImage();
        currentSelect = _EMainSelect_.emsStart;
        SetArrow();
        optionFrame = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            SwitchArrow("Up");
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            SwitchArrow("Down");
        else if(Input.GetKeyDown(KeyCode.Return))
            EnterMenu();
    }

    public void StartGame()
    {
        mapLoader.StartLoadMap(_EMapType_.eIntro);
    }

    public void OnOffOption()
    {
        if(optionFrame.activeSelf)
        {
            optionFrame.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            optionFrame.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void SwitchArrow(string _upDown)
    {
        switch (currentSelect)
        {
            case _EMainSelect_.emsStart:
                currentSelect = _upDown == "Up" ? _EMainSelect_.emsQuit : _EMainSelect_.emsOption;
                break;
            case _EMainSelect_.emsOption:
                currentSelect = _upDown == "Up" ? _EMainSelect_.emsStart : _EMainSelect_.emsQuit;
                break;
            case _EMainSelect_.emsQuit:
                currentSelect = _upDown == "Up" ? _EMainSelect_.emsOption : _EMainSelect_.emsStart;
                break;
            default:
                break;
        }

        SetArrow();
    }

    void SetArrow()
    {
        switch (currentSelect)
        {
            case _EMainSelect_.emsStart:
                myAction = StartGame;
                break;
            case _EMainSelect_.emsOption:
                myAction = OnOffOption;
                break;
            case _EMainSelect_.emsQuit:
                myAction = QuitGame;
                break;
            default:
                break;
        }

        for (int i = 0; i < arrowGroup.Length; i++)
        {
            if (i == (int)currentSelect)
            {
                arrowGroup[i].SetActive(true);
                continue;
            }

            arrowGroup[i].SetActive(false);
        }
    }

    void EnterMenu()
    {
        myAction.Invoke();
    }
}
