using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBattleIdleState : MonsterBattleIdleState
{
    protected override IEnumerator AttackCoolDown()
    {
        while (true)
        {
            if (myStat.GetFloatStat(_EFloatStatType_.eCurrentAttackCool) > 0f)
                myStat.SetFloatStat(_EFloatStatType_.eCurrentAttackCool, -Time.deltaTime);

            yield return null;
        }
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Idle");
    }

    protected override IEnumerator CheckDistance()
    {
        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        while (true)
        {
            if (Vector3.Distance(target.position, transform.position) > myStat.GetFloatStat(_EFloatStatType_.eAttackRange))
                stateManager.SetActionType(_EStateType_.eChase, _EObjectType_.eMonster);
            else
            {
                if (myStat.GetFloatStat(_EFloatStatType_.eCurrentAttackCool) <= 0f)
                    stateManager.SetActionType(_EStateType_.eAttack, _EObjectType_.eMonster);
            }

            yield return null;
        }
    }

    protected override IEnumerator LookTarget()
    {
        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        while (true)
        {
            transform.LookAt(target);

            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));

            yield return null;
        }
    }
}
