using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : MonsterAttackState
{
    string animName;

    MonsterPattern myPattern;

    public override void DoAction(_EStateType_ state)
    {
        StartCoroutine(AfterAttack());
        PatternActivate();
    }

    void PatternActivate()
    {
        myPattern = GetComponent<MonsterPattern>();
        animName = myPattern.SetRandomSkill();
    }

    protected override IEnumerator AfterAttack()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(animName))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        myStat.SetFloatStat(
        _EFloatStatType_.eCurrentAttackCool,
        myStat.GetFloatStat(_EFloatStatType_.eAttackCool) + (myStat.GetFloatStat(_EFloatStatType_.eCurrentAttackCool) * -1));

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

        if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.eAttackRange))
            stateManager.SetActionType(_EStateType_.eBattleIdle, _EObjectType_.eMonster);
        else
            stateManager.SetActionType(_EStateType_.eChase, _EObjectType_.eMonster);
    }
}
