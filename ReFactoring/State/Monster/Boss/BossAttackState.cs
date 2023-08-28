using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : MonsterAttackState
{
    string m_animName;

    MonsterPattern myPattern;

    public override void DoAction(_EStateType_ _type)
    {
        StartCoroutine(AfterAttack());
        PatternActivate();
    }

    void PatternActivate()
    {
        myPattern = GetComponent<MonsterPattern>();
        m_animName = myPattern.SetRandomSkill();
    }

    protected override IEnumerator AfterAttack()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(m_animName))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        myStat.SetFloatStat(
        _EFloatStatType_.efstCurrentAttackCool,
        myStat.GetFloatStat(_EFloatStatType_.efstAttackCool) + (myStat.GetFloatStat(_EFloatStatType_.efstCurrentAttackCool) * -1));

        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                    target = col.transform;
            }
        }

        if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.efstAttackRange))
            stateManager.SetActionType(_EStateType_.estBattleIdle, _EObjectType_.eotMonster);
        else
            stateManager.SetActionType(_EStateType_.estChase, _EObjectType_.eotMonster);
    }
}
