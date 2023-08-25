using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoAStat : GumihoStat
{
    public GumihoAStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    protected override void InitStat()
    {
        myStat.maxHp = GUMIHO_A_MAX_HP;
        myStat.currentHp = GUMIHO_A_MAX_HP;
        myStat.damage = GUMIHO_DAMAGE;
        myStat.speed = GUMIHO_SPEED;
        myStat.sight = GUMIHO_SIGHT;
        myStat.attackRange = GUMIHO_ATTACKRANGE;
        myStat.attackCool = GUMIHO_ATTACKCOOL;
        myStat.currentAttackCool = GUMIHO_ATTACKCOOL;
    }
}
