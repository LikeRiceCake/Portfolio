using PublicStructs.Character;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

public class BirdStat : MonsterStat
{
    const int BIRD_MAX_HP = 10;
    const int BIRD_DAMAGE = 20;
    const float BIRD_SPEED = 1.5f;
    const float BIRD_SIGHT = 15f;
    const float BIRD_ATTACKRANGE = 15f;
    const float BIRD_ATTACKCOOL = 3f;

    public BirdStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    protected override void InitStat()
    {
        myStat.maxHp = BIRD_MAX_HP;
        myStat.currentHp = BIRD_MAX_HP;
        myStat.damage = BIRD_DAMAGE;
        myStat.speed = BIRD_SPEED;
        myStat.sight = BIRD_SIGHT;
        myStat.attackRange = BIRD_ATTACKRANGE;
        myStat.attackCool = BIRD_ATTACKCOOL;
        myStat.currentAttackCool = 0f;
    }
}
