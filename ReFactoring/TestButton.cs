using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject.Find("Player").GetComponent<IStat>().GetStat().UnderAttack(
                GameObject.Find("Player").GetComponent<IStat>().GetStat().GetIntStat(PublicEnums._EIntStatType_.eMaxHp));
        }
        
        if(Input.GetKeyDown(KeyCode.F1))
            GameObject.Find("Dialogue").GetComponent<DialogueEvent>().ReactNotify(_EDialogueEventType_.eTutorial);
    }
}
