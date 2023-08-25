using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _EHeoghoPattern_
{
    e100,
    e50,
    e30,
    eMax
}

public class HeoghoStat : MonsterStat
{
    const int HEOGHO_MAX_HP = 350;
    const int HEOGHO_DAMAGE = 10;
    const float HEOGHO_SPEED = 10f;
    const float HEOGHO_SIGHT = 999f;
    const float HEOGHO_ATTACKRANGE = 10f;
    const float HEOGHO_ATTACKCOOL = 5f;

    const int HEOGHO_PATTERN_HP_50 = HEOGHO_MAX_HP * 50 / 100;
    const int HEOGHO_PATTERN_HP_30 = HEOGHO_MAX_HP * 30 / 100;

    public HeoghoStat(StateManager stateManager) : base(stateManager)
    {
        base.stateManager = stateManager;
    }

    public Heogho myBoss { get; set; }

    public MonsterPattern mp { get; set; }

    public override void UnderAttack(int damage)
    {
        if (stateManager.currentState != _EStateType_.eDie)
        {
            myStat.currentHp -= damage;

            if (mp.GetNowPattern() != _EMonsterPattern_.eHeogho_30 && myStat.currentHp <= HEOGHO_PATTERN_HP_30)
                myBoss.ChangePattern(_EHeoghoPattern_.e30);
            else if (mp.GetNowPattern() != _EMonsterPattern_.eHeogho_50 && myStat.currentHp <= HEOGHO_PATTERN_HP_50)
                myBoss.ChangePattern(_EHeoghoPattern_.e50);
        }

        if (CheckDie())
            stateManager.SetActionType(_EStateType_.eDie, _EObjectType_.eMonster);
    }

    protected override void InitStat()
    {
        myStat.maxHp = HEOGHO_MAX_HP;
        myStat.currentHp = HEOGHO_MAX_HP;
        myStat.damage = HEOGHO_DAMAGE;
        myStat.speed = HEOGHO_SPEED;
        myStat.sight = HEOGHO_SIGHT;
        myStat.attackRange = HEOGHO_ATTACKRANGE;
        myStat.attackCool = HEOGHO_ATTACKCOOL;
        myStat.currentAttackCool = HEOGHO_ATTACKCOOL;
    }
}
