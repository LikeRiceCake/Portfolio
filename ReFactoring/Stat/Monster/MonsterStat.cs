using PublicEnums;
using PublicEnums.State;
using PublicStructs.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    const int MONSTER_MAX_HP = 1;
    const int MONSTER_DAMAGE = 1;
    const float MONSTER_SPEED = 1f;
    const float MONSTER_SIGHT = 1f;
    const float MONSTER_ATTACKRANGE = 1f;
    const float MONSTER_ATTACKCOOL = 1f;

    protected EnemyStat myStat;

    public MonsterStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    public override void UnderAttack(int damage)
    {
        if (stateManager.currentState != _EStateType_.eDie)
        {
            myStat.currentHp -= damage;

            if (CheckDie())
                stateManager.SetActionType(_EStateType_.eDie, _EObjectType_.eMonster);
        }
    }

    public override int GetDamage(_EIntStatType_ select)
    {
        switch (select)
        {
            case _EIntStatType_.eDamage:
            case _EIntStatType_.eDamage_Sec:
            case _EIntStatType_.eDamage_Thi:
            case _EIntStatType_.eDamage_For:
            case _EIntStatType_.eDamage_Fif:
            case _EIntStatType_.eDamage_Six:
                return myStat.damage;
        }

        return 0;
    }

    public override int GetIntStat(_EIntStatType_ select)
    {
        switch (select)
        {
            case _EIntStatType_.eMaxHp:
                return myStat.maxHp;
            case _EIntStatType_.eCurrentHp:
                return myStat.currentHp;
        }

        return 0;
    }

    protected override void InitStat()
    {
        myStat.maxHp = MONSTER_MAX_HP;
        myStat.currentHp = MONSTER_MAX_HP;

        myStat.damage = MONSTER_DAMAGE;

        myStat.speed = MONSTER_SPEED;

        myStat.sight = MONSTER_SIGHT;

        myStat.attackRange = MONSTER_ATTACKRANGE;

        myStat.attackCool = MONSTER_ATTACKCOOL;

        myStat.currentAttackCool = 0f;
    }

    public override void PlusHp(_EIntStatType_ select, int value)
    {
        switch (select)
        {
            case _EIntStatType_.eMaxHp:
                myStat.maxHp += value;
                break;
            case _EIntStatType_.eCurrentHp:
                if (myStat.currentHp + value <= myStat.maxHp)
                    myStat.currentHp += value;
                else
                    myStat.currentHp = myStat.maxHp;
                break;
        }
    }

    public override void SetHp(_EIntStatType_ select, int value)
    {
        switch (select)
        {
            case _EIntStatType_.eMaxHp:
                myStat.maxHp = value;
                break;
            case _EIntStatType_.eCurrentHp:
                myStat.currentHp = value;
                break;
        }
    }

    protected override bool CheckDie()
    {
        if (myStat.currentHp <= 0)
            return true;

        return false;
    }

    public override float GetFloatStat(_EFloatStatType_ type)
    {
        switch (type)
        {
            case _EFloatStatType_.eSpeed:
                return myStat.speed;
            case _EFloatStatType_.eAttackRange:
                return myStat.attackRange;
            case _EFloatStatType_.eSight:
                return myStat.sight;
            case _EFloatStatType_.eAttackCool:
                return myStat.attackCool;
            case _EFloatStatType_.eCurrentAttackCool:
                return myStat.currentAttackCool;
        }

        return 0;
    }

    public override void SetFloatStat(_EFloatStatType_ type, float value)
    {
        switch (type)
        {
            case _EFloatStatType_.eCurrentAttackCool:
                myStat.currentAttackCool += value;
                break;
        }
    }
}
