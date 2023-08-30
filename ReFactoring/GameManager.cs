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

    bool m_isStop;
    bool m_isLine;
    bool m_isOption;

    void Start()
    {
        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();

        GameObject.Find("Manager").GetComponent<IGameStateSubject>().AddObserver(this);

        initHelper.needInit[(int)_EInitCallType_.eictEnter_InGameScene].Add(Init_Enter_InGameScene);

        optionFrame = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").gameObject;

        m_isStop = m_isLine = m_isOption = false;
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
        SetGameState(_EGameStateType_.egstIsOption, SwitchOptionFrame());
    }

    public void SetGameState(_EGameStateType_ type, bool _state)
    {
        switch (type)
        {
            case _EGameStateType_.egstIsStop:
                m_isStop = _state;
                break;
            case _EGameStateType_.egstIsLine:
                m_isLine = _state;
                break;
            case _EGameStateType_.egstIsOption:
                m_isOption = _state;
                break;
        }
        GameObject.Find("Player").GetComponent<PlayerStateManager>().SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
    }

    public bool GetGameState(_EGameStateType_ _type)
    {
        switch (_type)
        {
            case _EGameStateType_.egstIsStop:
                return m_isStop;
            case _EGameStateType_.egstIsLine:
                return m_isLine;
            case _EGameStateType_.egstIsOption:
                return m_isOption;
        }

        return false;
    }

    public bool CheckMoveable()
    {
        if (m_isStop || m_isOption || m_isLine)
            return false;

        return true;
    }

    public void ReactNotify(_EInputType_ _type, _EInputDetailType_ _dType)
    {
        if(_type == _EInputType_.eitOption)
        {
            switch (_dType)
            {
                case _EInputDetailType_.eidtOption:
                    SetGameState(_EGameStateType_.egstIsOption, SwitchOptionFrame());
                    break;
            }
        }
    }

    public void ReactNotify(_EGameStateType_ _type, bool _state)
    {
        SetGameState(_type, _state);
    }

    public void Init_Enter_InGameScene()
    {
        GameObject.Find("Player")?.GetComponent<IInputKeyClickSubject>().AddObserver(this);

        GameObject.Find("Dialogue")?.GetComponent<IGameStateSubject>().AddObserver(this);
    }
}
