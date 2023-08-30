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
        erptTutorialRoom,
        erptMiddleBossRoom,
        erptFinalBossRoom,
        erptMax
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

    public void SetRespawnPos(Vector3 _pos)
    {
        respawnPos = _pos;
    }

    public void SetPlayer(GameObject _player)
    {
        this.player = _player;
    }

    public bool DoRespawn()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CharacterController>().transform.position = respawnPos;
        player.GetComponent<CharacterController>().enabled = true;

        player.GetComponent<IStat>().GetStat().SetHp(
            _EIntStatType_.eistCurrentHp,
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistMaxHp) - player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistCurrentHp));

        foreach (var monsterList in monsterSpawner.GetMonsters())
        {
            for(int i = 0; i < monsterList.Length; i++)
            {
                if (monsterList[i] != null)
                {
                    monsterList[i].GetComponent<IStat>().GetStat().SetHp(
                        _EIntStatType_.eistCurrentHp,
                        monsterList[i].GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistMaxHp) - monsterList[i].GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistCurrentHp));

                    monsterList[i].GetComponent<StateManager>().SetActionType(_EStateType_.estIdle, _EObjectType_.eotMonster);
                }   
            }
        }

        player.GetComponent<StateManager>().SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);

        characterHPUI.ChangeCharacterHPUI(_EHPUIType_.ehutPlayer,
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistCurrentHp),
            player.GetComponent<IStat>().GetStat().GetIntStat(_EIntStatType_.eistMaxHp));

        return true;
    }
}