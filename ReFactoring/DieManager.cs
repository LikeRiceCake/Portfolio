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

        initHelper.needInit[(int)_EInitCallType_.eEnter_InGameScene].Add(Init_Enter_InGameScene);
    }

    public void AddMyFunc(IDieSubject die)
    {
        die.AddObserver(this);
    }

    public void ReactNotify(_EObjectType_ type)
    {
        switch (type)
        {
            case _EObjectType_.ePlayer:
                spriteRunner.SetRunImage("Loading_Image");
                spriteRunner.OnImage();
                spriteRunner.RunImage();
                if(playerRespawn.DoRespawn())
                {
                    spriteRunner.StopImage();
                    spriteRunner.OffImage();
                }
                break;
            case _EObjectType_.eNormal:
            case _EObjectType_.eBoss:
                Notify(_EDialogueEventType_.eMonsterDie);
                break;
        }
    }

    private void Awake()
    {
        myObs = new List<IDialogueEventObserver>();
    }

    public void AddObserver(IDialogueEventObserver ob)
    {
        myObs.Add(ob);
    }

    public void Notify(_EDialogueEventType_ type)
    {
        foreach (var ob in myObs)
            ob.ReactNotify(type);
    }

    public void RemoveObserver(IDialogueEventObserver ob)
    {
        myObs.Remove(ob);
    }

    public void Init_Enter_InGameScene()
    {
        playerRespawn = GameObject.Find("PlayerRespawn")?.GetComponent<PlayerRespawn>();

        GameObject.Find("Player")?.GetComponent<IDieSubject>().AddObserver(this);
    }
}
