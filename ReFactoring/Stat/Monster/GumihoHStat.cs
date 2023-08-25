using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoHStat : GumihoStat
{
    public GumihoHStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    public override void UnderAttack(int damage)
    {
        if (stateManager.currentState != _EStateType_.eDie)
        {
            myStat.currentHp -= damage;

            if (myStat.currentHp <= GUMIHO_A_MAX_HP)
                stateManager.SetActionType(_EStateType_.eDie, _EObjectType_.eMonster);
        }
    }

    protected override void InitStat()
    {
        myStat.maxHp = GUMIHO_H_MAX_HP;
        myStat.currentHp = GUMIHO_H_MAX_HP;
        myStat.damage = GUMIHO_DAMAGE;
        myStat.speed = GUMIHO_SPEED;
        myStat.sight = GUMIHO_SIGHT;
        myStat.attackRange = GUMIHO_ATTACKRANGE;
        myStat.attackCool = GUMIHO_ATTACKCOOL;
        myStat.currentAttackCool = GUMIHO_ATTACKCOOL;
    }
}
