using PublicEnums;
using PublicEnums.State;
using PublicEnums.UI.HP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    enum _ERespawnPosType_
    {
        eTutorialRoom,
        eMiddleBossRoom,
        eFinalBossRoom,
        eMax
    }

    GameObject player;

    CharacterHPUI characterHPUI;

    Vector3 respawnPos;

    MonsterSpawner monsterSpawner;

    void Start()
    {
        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        characterHPUI = GameObject.Find("CharacterHP").GetComponent<CharacterHPUI>();
    }

    public void SetRespawnPos(Vector3 pos)
    {
        respawnPos = pos;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public bool DoRespawn()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CharacterController>().transform.position = respawnPos;
        player.GetComponent<CharacterController>().enabled = true;

        player.GetComponent<IStat>().GetStat().SetHp(
            _EIntStatType_.eCurrentHp,
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eMaxHp) - player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eCurrentHp));

        foreach (var monsterList in monsterSpawner.GetMonsters())
        {
            for(int i = 0; i < monsterList.Length; i++)
            {
                if (monsterList[i] != null)
                {
                    monsterList[i].GetComponent<IStat>().GetStat().SetHp(
                        _EIntStatType_.eCurrentHp,
                        monsterList[i].GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eMaxHp) - monsterList[i].GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eCurrentHp));

                    monsterList[i].GetComponent<StateManager>().SetActionType(_EStateType_.eIdle, _EObjectType_.eMonster);
                }   
            }
        }

        player.GetComponent<StateManager>().SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);

        characterHPUI.ChangeCharacterHPUI(_EHPUIType_.ePlayer,
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eCurrentHp),
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eMaxHp));

        return true;
    }
}