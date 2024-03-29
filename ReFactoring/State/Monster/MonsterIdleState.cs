using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterIdleState : MonsterState
{
    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        myAnim.SetTrigger("Idle");
        StartCoroutine(SearchAround());
    }

    protected abstract IEnumerator SearchAround();
}
