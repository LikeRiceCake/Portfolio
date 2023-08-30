using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoAStat : GumihoStat
{
    public GumihoAStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    protected override void InitStat()
    {
        myStat.maxHp = GUMIHO_A_MAX_HP;
        myStat.currentHp = GUMIHO_A_MAX_HP;
        myStat.damage = GUMIHO_DAMAGE;
        myStat.speed = GUMIHO_SPEED;
        myStat.sight = GUMIHO_SIGHT;
        myStat.attackRange = GUMIHO_ATTACK_RANGE;
        myStat.attackCool = GUMIHO_ATTACK_COOL;
        myStat.currentAttackCool = GUMIHO_ATTACK_COOL;
    }
}
