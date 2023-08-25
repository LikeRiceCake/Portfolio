using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour, IInputKeyClickObserver, IGameStateObserver
{
    GameObject optionFrame;

    InitHelper initHelper;

    bool isStop;
    bool isLine;
    bool isOption;

    void Start()
    {
        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();

        GameObject.Find("Manager").GetComponent<IGameStateSubject>().AddObserver(this);

        initHelper.needInit[(int)_EInitCallType_.eEnter_InGameScene].Add(Init_Enter_InGameScene);

        optionFrame = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").gameObject;

        isStop = isLine = isOption = false;
    }

    bool SwitchOptionFrame()
    {
        if (optionFrame.activeSelf)
            optionFrame.SetActive(false);
        else
            optionFrame.SetActive(true);

        return optionFrame.activeSelf;
    }

    public void ControlOption()
    {
        SetGameState(_EGameStateType_.eisOption, SwitchOptionFrame());
    }

    public void SetGameState(_EGameStateType_ type, bool state)
    {
        switch (type)
        {
            case _EGameStateType_.eisStop:
                isStop = state;
                break;
            case _EGameStateType_.eisLine:
                isLine = state;
                break;
            case _EGameStateType_.eisOption:
                isOption = state;
                break;
        }
        GameObject.Find("Player").GetComponent<PlayerStateManager>().SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);
    }

    public bool GetGameState(_EGameStateType_ type)
    {
        switch (type)
        {
            case _EGameStateType_.eisStop:
                return isStop;
            case _EGameStateType_.eisLine:
                return isLine;
            case _EGameStateType_.eisOption:
                return isOption;
        }

        return false;
    }

    public bool CheckMoveable()
    {
        if (isStop || isOption || isLine)
            return false;

        return true;
    }

    public void ReactNotify(_EInputType_ type, _EInputDetailType_ dType)
    {
        if(type == _EInputType_.eOption)
        {
            switch (dType)
            {
                case _EInputDetailType_.eOption:
                    SetGameState(_EGameStateType_.eisOption, SwitchOptionFrame());
                    break;
            }
        }
    }

    public void ReactNotify(_EGameStateType_ type, bool state)
    {
        SetGameState(type, state);
    }

    public void Init_Enter_InGameScene()
    {
        GameObject.Find("Player")?.GetComponent<IInputKeyClickSubject>().AddObserver(this);

        GameObject.Find("Dialogue")?.GetComponent<IGameStateSubject>().AddObserver(this);
    }
}
