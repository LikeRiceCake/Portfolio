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
    const float HEOGHO_ATTACK_RANGE = 10f;
    const float HEOGHO_ATTACK_COOL = 5f;

    const int HEOGHO_PATTERN_HP_50 = HEOGHO_MAX_HP * 50 / 100;
    const int HEOGHO_PATTERN_HP_30 = HEOGHO_MAX_HP * 30 / 100;

    public HeoghoStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }

    public Heogho myBoss { get; set; }

    public MonsterPattern mp { get; set; }

    public override void UnderAttack(int _damage)
    {
        if (stateManager.currentState != _EStateType_.estDie)
        {
            myStat.currentHp -= _damage;

            if (mp.GetNowPattern() != _EMonsterPattern_.empHeogho30 && myStat.currentHp <= HEOGHO_PATTERN_HP_30)
                myBoss.ChangePattern(_EHeoghoPattern_.e30);
            else if (mp.GetNowPattern() != _EMonsterPattern_.empHeogho50 && myStat.currentHp <= HEOGHO_PATTERN_HP_50)
                myBoss.ChangePattern(_EHeoghoPattern_.e50);
        }

        if (CheckDie())
            stateManager.SetActionType(_EStateType_.estDie, _EObjectType_.eotMonster);
    }

    protected override void InitStat()
    {
        myStat.maxHp = HEOGHO_MAX_HP;
        myStat.currentHp = HEOGHO_MAX_HP;
        myStat.damage = HEOGHO_DAMAGE;
        myStat.speed = HEOGHO_SPEED;
        myStat.sight = HEOGHO_SIGHT;
        myStat.attackRange = HEOGHO_ATTACK_RANGE;
        myStat.attackCool = HEOGHO_ATTACK_COOL;
        myStat.currentAttackCool = HEOGHO_ATTACK_COOL;
    }
}
