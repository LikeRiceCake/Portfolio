using System.Collections;
using System;
using System.Collections.Generic;
using PublicStructs.Character;
using UnityEngine;
using PublicEnums.State;
using PublicEnums;
using Unity.VisualScripting;
using PublicEnums.UI.HP;
using Unity.Mathematics;

public class Player : MonoBehaviour, IStat
{
    ControlStrategy myControl;

    Stat stat;

    PlayerRespawn myRespawn;

    private void Awake()
    {
        myRespawn = GameObject.Find("PlayerRespawn").GetComponent<PlayerRespawn>();

        myRespawn.SetRespawnPos(GameObject.Find("TutorialRoomRespawnPosition").transform.position);

        myRespawn.SetPlayer(gameObject);

        myControl = gameObject.AddComponent<PlayerInputStrategy>();

        stat = new PlayerStat(GetComponent<PlayerStateManager>());

        ((PlayerStat)stat).hpUI = GameObject.Find("CharacterHP").GetComponent<CharacterHPUI>();
    }

    void Start()
    {
        myControl.ControlAction();
    }

    public Stat GetStat()
    {
        return stat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BeforeMiddleBossRoom"))
            myRespawn.SetRespawnPos(GameObject.Find("MiddleBossRoomRespawnPosition").transform.position);
        else if (other.transform.CompareTag("BeforeFinalBossRoom"))
            myRespawn.SetRespawnPos(GameObject.Find("FinalBossRoomRespawnPosition").transform.position);
    }
}
