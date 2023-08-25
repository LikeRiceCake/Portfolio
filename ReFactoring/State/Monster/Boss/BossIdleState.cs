using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : MonsterIdleState
{
    const float CHANGE_MOVEMENT = 3.5f;

    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        Invoke("ChangeMovement", CHANGE_MOVEMENT);
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Idle");
    }

    protected override IEnumerator SearchAround()
    {
        while (true)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("Player"))
                        stateManager.SetActionType(_EStateType_.eChase, _EObjectType_.eMonster);
                }
            }

            yield return null;
        }
    }

    void ChangeMovement()
    {
        stateManager.SetActionType(_EStateType_.eMove, _EObjectType_.eMonster);
    }
}
