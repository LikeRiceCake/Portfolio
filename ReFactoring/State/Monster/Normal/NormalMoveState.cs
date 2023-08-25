using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMoveState : MonsterMoveState
{
    const float CHANGE_MOVEMENT = 1f;

    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        Invoke("ChangeMovement", CHANGE_MOVEMENT);
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Move");
    }

    protected override IEnumerator MoveAround()
    {
        float angle = Random.Range(0f, 360f);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, angle, transform.rotation.z));

        while (true)
        {
            transform.position += transform.forward * myStat.GetFloatStat(_EFloatStatType_.eSpeed) * Time.deltaTime;

            yield return null;
        }
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
        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.eMonster);
    }
}
