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

    public override void DoAction(_EStateType_ _type)
    {
        switch (me.GetMonsterType())
        {
            case _EMonsterType_.emtGumihoH:
                mapLoader.StartLoadMap(_EMapType_.emtMiddleBossTransformation);
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstMiddleBossRoom_A);
                break; 
            case _EMonsterType_.emtGumihoA:
                stateManager.NotifyDeath();
                break;
            case _EMonsterType_.emtHeogho:
                stateManager.NotifyDeath();
                break;
        }

        Destroy(gameObject);
    }
}
