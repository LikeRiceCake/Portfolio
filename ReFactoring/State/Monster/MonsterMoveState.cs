using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMoveState : MonsterState
{
    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);
        myAnim.SetTrigger("Move");
        StartCoroutine(SearchAround());
        StartCoroutine(MoveAround());
    }

    protected abstract IEnumerator MoveAround();
    protected abstract IEnumerator SearchAround();

    
}
