using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class FixedSkill : MonsterSkill
{
    protected abstract void BehaviourFunc();
}
