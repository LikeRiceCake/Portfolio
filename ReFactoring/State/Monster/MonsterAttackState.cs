using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttackState : MonsterState
{
    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);
        myAnim.SetTrigger("Attack");
        StartCoroutine(AfterAttack());
    }

    protected abstract IEnumerator AfterAttack();
}
