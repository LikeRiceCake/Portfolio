using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalIdleState : MonsterIdleState
{
    const float CHANGE_MOVEMENT = 3.5f;

    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);
        Invoke("ChangeMovement", CHANGE_MOVEMENT);
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Idle");
    }

    protected override IEnumerator SearchAround()
    {
        while(true)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

            if (cols != null)
            {
                foreach(var col in cols)
                {
                    if (col.CompareTag("Player"))
                        stateManager.SetActionType(_EStateType_.estChase, _EObjectType_.eotMonster);
                }
            }

            yield return null;
        }
    }

    void ChangeMovement()
    {
        stateManager.SetActionType(_EStateType_.estMove, _EObjectType_.eotMonster);
    }
}
