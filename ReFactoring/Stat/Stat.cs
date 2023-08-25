using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat
{
    protected StateManager stateManager;

    public Stat(StateManager stateManager)
    {
        this.stateManager = stateManager;

        InitStat();
    }

    protected abstract bool CheckDie();

    public abstract void UnderAttack(int damage);

    public abstract int GetDamage(_EIntStatType_ select);

    public abstract int GetIntStat(_EIntStatType_ select);

    protected abstract void InitStat();

    public abstract void PlusHp(_EIntStatType_ select, int value);

    public abstract void SetHp(_EIntStatType_ select, int value);

    public abstract float GetFloatStat(_EFloatStatType_ type);

    public abstract void SetFloatStat(_EFloatStatType_ type, float value);
}
