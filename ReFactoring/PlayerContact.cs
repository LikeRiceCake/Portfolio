using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContact : MonoBehaviour, IDialogueEventSubject
{
    List<IDialogueEventObserver> myObs = new List<IDialogueEventObserver>();

    private void Awake()
    {
        
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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.transform.CompareTag("Player"))
        {
            switch (gameObject.tag)
            {
                case "Tutorial":
                    Notify(_EDialogueEventType_.edetTutorial);
                    break;
                case "BeforeSecondRoom":
                    Notify(_EDialogueEventType_.edetBeforeSecondRoom);
                    break;
                case "FrontNotBrokenWall":
                    Notify(_EDialogueEventType_.edetFrontNotBrokenWall);
                    break;
                case "BeforeMiddleBossRoom":
                    Notify(_EDialogueEventType_.edetBeforeMiddleBossRoom);
                    break;
                case "EnterMiddleBossRoom":
                    Notify(_EDialogueEventType_.edetEnterMiddleBossRoom);
                    break;
                case "BeforeFinalBossRoom":
                    Notify(_EDialogueEventType_.edetBeforeFinalBossRoom);
                    break;
                case "FrontCanBrokenWall":
                    Notify(_EDialogueEventType_.edetFrontCanBrokenWall);
                    break;
                case "FrontAnotherWall":
                    Notify(_EDialogueEventType_.edetFrontAnotherWall);
                    break;
                case "PondCrystal":
                    Notify(_EDialogueEventType_.edetPondCrystal);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
