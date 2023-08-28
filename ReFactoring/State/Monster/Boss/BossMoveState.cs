using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : MonsterMoveState
{
    const float CHANGE_MOVEMENT_TIME = 3.5f;

    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);
        Invoke("ChangeMovement", CHANGE_MOVEMENT_TIME);
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Move");
    }

    protected override IEnumerator MoveAround()
    {
        float angle = Random.Range(0f, 360f);

        transform.LookAt(new Vector3(transform.rotation.x, angle, transform.rotation.z));

        while (true)
        {
            transform.position += transform.forward * myStat.GetFloatStat(_EFloatStatType_.efstSpeed) * Time.deltaTime;

            yield return null;
        }
    }

    protected override IEnumerator SearchAround()
    {
        while (true)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

            if (cols != null)
            {
                foreach (var col in cols)
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
