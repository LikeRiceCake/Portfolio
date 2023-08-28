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
    const float MONSTER_ATTACK_RANGE = 1f;
    const float MONSTER_ATTACK_COOL = 1f;

    protected EnemyStat myStat;

    public MonsterStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    public override void UnderAttack(int _damage)
    {
        if (stateManager.currentState != _EStateType_.estDie)
        {
            myStat.currentHp -= _damage;

            if (CheckDie())
                stateManager.SetActionType(_EStateType_.estDie, _EObjectType_.eotMonster);
        }
    }

    public override int GetDamage(_EIntStatType_ _type)
    {
        switch (_type)
        {
            case _EIntStatType_.eistDamage:
            case _EIntStatType_.eistDamage_Sec:
            case _EIntStatType_.eistDamage_Thi:
            case _EIntStatType_.eistDamage_For:
            case _EIntStatType_.eistDamage_Fif:
            case _EIntStatType_.eistDamage_Six:
                return myStat.damage;
        }

        return 0;
    }

    public override int GetIntStat(_EIntStatType_ _type)
    {
        switch (_type)
        {
            case _EIntStatType_.eistMaxHp:
                return myStat.maxHp;
            case _EIntStatType_.eistCurrentHp:
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

        myStat.attackRange = MONSTER_ATTACK_RANGE;

        myStat.attackCool = MONSTER_ATTACK_COOL;

        myStat.currentAttackCool = 0f;
    }

    public override void PlusHp(_EIntStatType_ _type, int _value)
    {
        switch (_type)
        {
            case _EIntStatType_.eistMaxHp:
                myStat.maxHp += _value;
                break;
            case _EIntStatType_.eistCurrentHp:
                if (myStat.currentHp + _value <= myStat.maxHp)
                    myStat.currentHp += _value;
                else
                    myStat.currentHp = myStat.maxHp;
                break;
        }
    }

    public override void SetHp(_EIntStatType_ _type, int _value)
    {
        switch (_type)
        {
            case _EIntStatType_.eistMaxHp:
                myStat.maxHp = _value;
                break;
            case _EIntStatType_.eistCurrentHp:
                myStat.currentHp = _value;
                break;
        }
    }

    protected override bool CheckDie()
    {
        if (myStat.currentHp <= 0)
            return true;

        return false;
    }

    public override float GetFloatStat(_EFloatStatType_ _type)
    {
        switch (_type)
        {
            case _EFloatStatType_.efstSpeed:
                return myStat.speed;
            case _EFloatStatType_.efstAttackRange:
                return myStat.attackRange;
            case _EFloatStatType_.efstSight:
                return myStat.sight;
            case _EFloatStatType_.efstAttackCool:
                return myStat.attackCool;
            case _EFloatStatType_.efstCurrentAttackCool:
                return myStat.currentAttackCool;
        }

        return 0;
    }

    public override void SetFloatStat(_EFloatStatType_ _type, float _value)
    {
        switch (_type)
        {
            case _EFloatStatType_.efstCurrentAttackCool:
                myStat.currentAttackCool += _value;
                break;
        }
    }
}
