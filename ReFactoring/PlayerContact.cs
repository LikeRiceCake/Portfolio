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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            switch (gameObject.tag)
            {
                case "Tutorial":
                    Notify(_EDialogueEventType_.eTutorial);
                    break;
                case "BeforeSecondRoom":
                    Notify(_EDialogueEventType_.eBeforeSecondRoom);
                    break;
                case "FrontNotBrokenWall":
                    Notify(_EDialogueEventType_.eFrontNotBrokenWall);
                    break;
                case "BeforeMiddleBossRoom":
                    Notify(_EDialogueEventType_.eBeforeMiddleBossRoom);
                    break;
                case "EnterMiddleBossRoom":
                    Notify(_EDialogueEventType_.eEnterMiddleBossRoom);
                    break;
                case "BeforeFinalBossRoom":
                    Notify(_EDialogueEventType_.eBeforeFinalBossRoom);
                    break;
                case "FrontCanBrokenWall":
                    Notify(_EDialogueEventType_.eFrontCanBrokenWall);
                    break;
                case "FrontAnotherWall":
                    Notify(_EDialogueEventType_.eFrontAnotherWall);
                    break;
                case "PondCrystal":
                    Notify(_EDialogueEventType_.ePondCrystal);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
