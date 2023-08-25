using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttackState : MonsterState
{
    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        myAnim.SetTrigger("Attack");
        StartCoroutine(AfterAttack());
    }

    protected abstract IEnumerator AfterAttack();
}
