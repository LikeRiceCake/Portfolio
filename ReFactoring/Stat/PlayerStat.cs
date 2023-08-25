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
    readonly int[] PLAYER_DAMAGE = { 30, 4, 5, 5, 7, 10 };
    const float PLAYER_SPEED = 15f;

    CharacterStat myStat;

    public CharacterHPUI hpUI { get; set; }

    public PlayerStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    protected override bool CheckDie()
    {
        if (myStat.currentHp <= 0)
            return true;

        return false;
    }

    public override void UnderAttack(int damage)
    {
        if (stateManager.currentState != _EStateType_.eDie)
        {
            myStat.currentHp -= damage;

            hpUI.ChangeCharacterHPUI(
                _EHPUIType_.ePlayer,
                myStat.currentHp,
                myStat.maxHp);

            if (CheckDie())
                stateManager.SetActionType(_EStateType_.eDie, _EObjectType_.ePlayer);
        }
    }

    public override int GetDamage(_EIntStatType_ select)
    {
        switch (select)
        {
            case _EIntStatType_.eDamage:
                return myStat.damage[_EIntStatType_.eDamage - _EIntStatType_.eDamage];
            case _EIntStatType_.eDamage_Sec:
                return myStat.damage[_EIntStatType_.eDamage_Sec - _EIntStatType_.eDamage];
            case _EIntStatType_.eDamage_Thi:
                return myStat.damage[_EIntStatType_.eDamage_Thi - _EIntStatType_.eDamage];
            case _EIntStatType_.eDamage_For:
                return myStat.damage[_EIntStatType_.eDamage_For - _EIntStatType_.eDamage];
            case _EIntStatType_.eDamage_Fif:
                return myStat.damage[_EIntStatType_.eDamage_Fif - _EIntStatType_.eDamage];
            case _EIntStatType_.eDamage_Six:
                return myStat.damage[_EIntStatType_.eDamage_Six - _EIntStatType_.eDamage];
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
        myStat.maxHp = PLAYER_MAX_HP;
        myStat.currentHp = PLAYER_MAX_HP;

        myStat.damage = new int[(int)_EPlayerDamageType_.eMax];

        myStat.damage[(int)_EPlayerDamageType_.eWeak_1] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eWeak_1];
        myStat.damage[(int)_EPlayerDamageType_.eWeak_2] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eWeak_2];
        myStat.damage[(int)_EPlayerDamageType_.eWeak_3] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eWeak_3];
        myStat.damage[(int)_EPlayerDamageType_.eW1_Strong_1] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eW1_Strong_1];
        myStat.damage[(int)_EPlayerDamageType_.eW2_Strong_2] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eW2_Strong_2];
        myStat.damage[(int)_EPlayerDamageType_.eW3_Strong_3] = PLAYER_DAMAGE[(int)_EPlayerDamageType_.eW3_Strong_3];

        myStat.speed = PLAYER_SPEED;
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

    public override float GetFloatStat(_EFloatStatType_ type)
    {
        switch (type)
        {
            case _EFloatStatType_.eSpeed:
                return myStat.speed;
        }

        return 0;
    }

    public override void SetFloatStat(_EFloatStatType_ type, float value)
    {
        switch (type)
        {
            default:
                break;
        }
    }
}
