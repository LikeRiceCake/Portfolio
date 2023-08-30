using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoHStat : GumihoStat
{
    public GumihoHStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    public override void UnderAttack(int _damage)
    {
        if (stateManager.currentState != _EStateType_.estDie)
        {
            myStat.currentHp -= _damage;

            if (myStat.currentHp <= GUMIHO_A_MAX_HP)
                stateManager.SetActionType(_EStateType_.estDie, _EObjectType_.eotMonster);
        }
    }

    protected override void InitStat()
    {
        myStat.maxHp = GUMIHO_H_MAX_HP;
        myStat.currentHp = GUMIHO_H_MAX_HP;
        myStat.damage = GUMIHO_DAMAGE;
        myStat.speed = GUMIHO_SPEED;
        myStat.sight = GUMIHO_SIGHT;
        myStat.attackRange = GUMIHO_ATTACK_RANGE;
        myStat.attackCool = GUMIHO_ATTACK_COOL;
        myStat.currentAttackCool = GUMIHO_ATTACK_COOL;
    }
}
