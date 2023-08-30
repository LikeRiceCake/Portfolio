using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat
{
    protected StateManager stateManager;

    public Stat(StateManager _stateManager)
    {
        this.stateManager = _stateManager;

        InitStat();
    }

    protected abstract bool CheckDie();

    public abstract void UnderAttack(int _damage);

    public abstract int GetDamage(_EIntStatType_ _type);

    public abstract int GetIntStat(_EIntStatType_ _type);

    protected abstract void InitStat();

    public abstract void PlusHp(_EIntStatType_ _type, int _value);

    public abstract void SetHp(_EIntStatType_ _type, int _value);

    public abstract float GetFloatStat(_EFloatStatType_ _type);

    public abstract void SetFloatStat(_EFloatStatType_ _type, float _value);
}
