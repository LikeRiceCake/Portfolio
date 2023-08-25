using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums;

public class BossDieState : MonsterDieState
{
    MapLoader mapLoader;

    MonsterSpawner monsterSpawner;

    Monster me;

    protected override void Awake()
    {
        base.Awake();

        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();

        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        me = GetComponent<Monster>();
    }

    public override void DoAction(_EStateType_ state)
    {
        switch (me.GetMonsterType())
        {
            case _EMonsterType_.eGumihoH:
                mapLoader.StartLoadMap(_EMapType_.eMiddleBossTransformation);
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eMiddleBossRoom_A);
                break; 
            case _EMonsterType_.eGumihoA:
                stateManager.NotifyDeath();
                break;
            case _EMonsterType_.eHeogho:
                stateManager.NotifyDeath();
                break;
        }

        Destroy(gameObject);
    }
}
