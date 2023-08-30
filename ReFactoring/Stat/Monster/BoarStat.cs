using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarStat : MonsterStat
{
    const int BOAR_MAX_HP = 20;
    const int BOAR_DAMAGE = 20;
    const float BOAR_SPEED = 1f;
    const float BOAR_SIGHT = 5f;
    const float BOAR_ATTACK_RANGE = 1.5f;
    const float BOAR_ATTACK_COOL = 2f;

    public BoarStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    protected override void InitStat()
    {
        myStat.maxHp = BOAR_MAX_HP;
        myStat.currentHp = BOAR_MAX_HP;
        myStat.damage = BOAR_DAMAGE;
        myStat.speed = BOAR_SPEED;
        myStat.sight = BOAR_SIGHT;
        myStat.attackRange = BOAR_ATTACK_RANGE;
        myStat.attackCool = BOAR_ATTACK_COOL;
        myStat.currentAttackCool = 0f;
    }
}
