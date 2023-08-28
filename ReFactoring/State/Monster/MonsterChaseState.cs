using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterChaseState : MonsterState
{
    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);
        myAnim.SetTrigger("Move");
        StartCoroutine(ChaseTarget());
        StartCoroutine(AttackCoolDown());
        StartCoroutine(CheckDistance());
    }

    protected abstract IEnumerator ChaseTarget();
    protected abstract IEnumerator AttackCoolDown();
    protected abstract IEnumerator CheckDistance();
}
