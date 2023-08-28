using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContact : MonoBehaviour, IDialogueEventSubject
{
    List<IDialogueEventObserver> myObs;

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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.transform.CompareTag("PlayerSkill"))
        {
            switch (transform.tag)
            {
                case "SkillWall(CanBrake)":
                    Destroy(_other.gameObject);
                    Notify(_EDialogueEventType_.edetEndBrakeWall);
                    gameObject.SetActive(false);
                    break;
                case "SkillWall(CantBrake)":
                    Destroy(_other.gameObject);
                    Notify(_EDialogueEventType_.edetEndShotWall);
                    break;
                case "PondCrystal":
                    Destroy(_other.gameObject);
                    Notify(_EDialogueEventType_.edetPondCrystal);
                    break;
            }
        }
    }
}
