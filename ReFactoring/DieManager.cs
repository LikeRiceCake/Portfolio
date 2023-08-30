using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManager : MonoBehaviour, IDieObserver, IDialogueEventSubject
{
    PlayerRespawn playerRespawn;
    SpriteAnimationRunner spriteRunner;

    InitHelper initHelper;

    List<IDialogueEventObserver> myObs;

    void Start()
    {
        spriteRunner = GameObject.Find("UI").GetComponent<SpriteAnimationRunner>();

        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();

        initHelper.needInit[(int)_EInitCallType_.eictEnter_InGameScene].Add(Init_Enter_InGameScene);
    }

    public void AddMyFunc(IDieSubject _die)
    {
        _die.AddObserver(this);
    }

    public void ReactNotify(_EObjectType_ _type)
    {
        switch (_type)
        {
            case _EObjectType_.eotPlayer:
                spriteRunner.SetRunImage("Loading_Image");
                spriteRunner.OnImage();
                spriteRunner.RunImage();
                if(playerRespawn.DoRespawn())
                {
                    spriteRunner.StopImage();
                    spriteRunner.OffImage();
                }
                break;
            case _EObjectType_.eotNormal:
            case _EObjectType_.eotBoss:
                Notify(_EDialogueEventType_.edetMonsterDie);
                break;
        }
    }

    private void Awake()
    {
        myObs = new List<IDialogueEventObserver>();
    }

    public void AddObserver(IDialogueEventObserver _ob)
    {
        myObs.Add(_ob);
    }

    public void Notify(_EDialogueEventType_ _type)
    {
        foreach (var ob in myObs)
            ob.ReactNotify(_type);
    }

    public void RemoveObserver(IDialogueEventObserver _ob)
    {
        myObs.Remove(_ob);
    }

    public void Init_Enter_InGameScene()
    {
        playerRespawn = GameObject.Find("PlayerRespawn")?.GetComponent<PlayerRespawn>();

        GameObject.Find("Player")?.GetComponent<IDieSubject>().AddObserver(this);
    }
}
