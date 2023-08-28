using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStat : MonsterStat
{
    const int WOLF_MAX_HP = 15;
    const int WOLF_DAMAGE = 15;
    const float WOLF_SPEED = 1.2f;
    const float WOLF_SIGHT = 5f;
    const float WOLF_ATTACK_RANGE = 1.5f;
    const float WOLF_ATTACK_COOL = 2f;

    public WolfStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    protected override void InitStat()
    {
        myStat.maxHp = WOLF_MAX_HP;
        myStat.currentHp = WOLF_MAX_HP;
        myStat.damage = WOLF_DAMAGE;
        myStat.speed = WOLF_SPEED;
        myStat.sight = WOLF_SIGHT;
        myStat.attackRange = WOLF_ATTACK_RANGE;
        myStat.attackCool = WOLF_ATTACK_COOL;
        myStat.currentAttackCool = 0f;
    }
}
