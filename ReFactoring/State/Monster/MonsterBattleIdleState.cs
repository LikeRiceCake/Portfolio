using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBattleIdleState : MonsterState
{
    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        myAnim.SetTrigger("Idle");
        StartCoroutine(AttackCoolDown());
        StartCoroutine(CheckDistance());
        StartCoroutine(LookTarget());
    }
    protected abstract IEnumerator AttackCoolDown();
    protected abstract IEnumerator CheckDistance();
    protected abstract IEnumerator LookTarget();
}
