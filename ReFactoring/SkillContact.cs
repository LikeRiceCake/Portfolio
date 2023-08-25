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
        if (other.transform.CompareTag("PlayerSkill"))
        {
            switch (transform.tag)
            {
                case "SkillWall(CanBrake)":
                    Destroy(other.gameObject);
                    Notify(_EDialogueEventType_.eEndBrakeWall);
                    gameObject.SetActive(false);
                    break;
                case "SkillWall(CantBrake)":
                    Destroy(other.gameObject);
                    Notify(_EDialogueEventType_.eEndShotWall);
                    break;
                case "PondCrystal":
                    Destroy(other.gameObject);
                    Notify(_EDialogueEventType_.ePondCrystal);
                    break;
            }
        }
    }
}
