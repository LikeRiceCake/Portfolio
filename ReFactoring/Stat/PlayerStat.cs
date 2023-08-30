using PublicEnums;
using PublicEnums.State;
using PublicEnums.UI.HP;
using PublicStructs.Character;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : Stat
{
    const int PLAYER_MAX_HP = 130;
    readonly int[] PLAYER_DAMAGES = { 30, 4, 5, 5, 7, 10 };
    const float PLAYER_SPEED = 15f;

    CharacterStat myStat;

    public CharacterHPUI hpUI { get; set; }

    public PlayerStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    protected override bool CheckDie()
    {
        if (myStat.currentHp <= 0)
            return true;

        return false;
    }

    public override void UnderAttack(int _damage)
    {
        if (stateManager.currentState != _EStateType_.estDie)
        {
            myStat.currentHp -= _damage;

            hpUI.ChangeCharacterHPUI(
                _EHPUIType_.ehutPlayer,
                myStat.currentHp,
                myStat.maxHp);

            if (CheckDie())
                stateManager.SetActionType(_EStateType_.estDie, _EObjectType_.eotPlayer);
        }
    }

    public override int GetDamage(_EIntStatType_ _type)
    {
        switch (_type)
        {
            case _EIntStatType_.eistDamage:
                return myStat.damage[_EIntStatType_.eistDamage - _EIntStatType_.eistDamage];
            case _EIntStatType_.eistDamage_Sec:
                return myStat.damage[_EIntStatType_.eistDamage_Sec - _EIntStatType_.eistDamage];
            case _EIntStatType_.eistDamage_Thi:
                return myStat.damage[_EIntStatType_.eistDamage_Thi - _EIntStatType_.eistDamage];
            case _EIntStatType_.eistDamage_For:
                return myStat.damage[_EIntStatType_.eistDamage_For - _EIntStatType_.eistDamage];
            case _EIntStatType_.eistDamage_Fif:
                return myStat.damage[_EIntStatType_.eistDamage_Fif - _EIntStatType_.eistDamage];
            case _EIntStatType_.eistDamage_Six:
                return myStat.damage[_EIntStatType_.eistDamage_Six - _EIntStatType_.eistDamage];
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
        myStat.maxHp = PLAYER_MAX_HP;
        myStat.currentHp = PLAYER_MAX_HP;

        myStat.damage = new int[(int)_EPlayerDamageType_.epdtMax];

        myStat.damage[(int)_EPlayerDamageType_.epdtWeak_1] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtWeak_1];
        myStat.damage[(int)_EPlayerDamageType_.epdtWeak_2] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtWeak_2];
        myStat.damage[(int)_EPlayerDamageType_.epdtWeak_3] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtWeak_3];
        myStat.damage[(int)_EPlayerDamageType_.epdtW1_Strong_1] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtW1_Strong_1];
        myStat.damage[(int)_EPlayerDamageType_.epdtW2_Strong_2] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtW2_Strong_2];
        myStat.damage[(int)_EPlayerDamageType_.epdtW3_Strong_3] = PLAYER_DAMAGES[(int)_EPlayerDamageType_.epdtW3_Strong_3];

        myStat.speed = PLAYER_SPEED;
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

    public override float GetFloatStat(_EFloatStatType_ type)
    {
        switch (type)
        {
            case _EFloatStatType_.efstSpeed:
                return myStat.speed;
        }

        return 0;
    }

    public override void SetFloatStat(_EFloatStatType_ type, float _value)
    {
        switch (type)
        {
            default:
                break;
        }
    }
}
