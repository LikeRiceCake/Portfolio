using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChaseState : MonsterChaseState
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
        myAnim.ResetTrigger("Move");
    }

    protected override IEnumerator ChaseTarget()
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

            transform.position += transform.forward * myStat.GetFloatStat(_EFloatStatType_.eSpeed) * Time.deltaTime;

            yield return null;
        }
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
            if(Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.eAttackRange))
            {
                if(myStat.GetFloatStat(_EFloatStatType_.eCurrentAttackCool) <= 0f)
                    stateManager.SetActionType(_EStateType_.eAttack, _EObjectType_.eMonster);
                else
                    stateManager.SetActionType(_EStateType_.eBattleIdle, _EObjectType_.eMonster);
            }    

            yield return null;
        }
    }
}
