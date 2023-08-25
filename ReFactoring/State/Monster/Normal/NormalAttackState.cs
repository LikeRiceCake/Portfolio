using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;

public class NormalAttackState : MonsterAttackState
{
    GameObject attackCollider;

    Vector3 offSet;

    const float ATTACK_COLLIDER_INS_DIS = 1.5f;
    const float ATTACK_COLLIDER_INS_HEIGHT = 0.5f;
    const float ATTACK_COLLIDER_SPEED = 5f;
    float attackColliderDestroyTime;

    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);

        ResourceManager resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        switch (GetComponent<Monster>().GetMonsterType())
        {
            case _EMonsterType_.eWolf:
            case _EMonsterType_.eBoar:
                attackCollider = resourceManager.LoadMonsterPrefab("Prefabs/SkillObject/CloseAttack");
                attackColliderDestroyTime = 0.1f;
                break;
            case _EMonsterType_.eBird:
                attackCollider = resourceManager.LoadMonsterPrefab("Prefabs/SkillObject/LongAttack");
                attackColliderDestroyTime = 5f;
                break;
        }

        offSet = new Vector3(0, ATTACK_COLLIDER_INS_HEIGHT, 0f);

        StartCoroutine(AttackColliderInstantiate());
    }

    IEnumerator AttackColliderInstantiate()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);

        GameObject obj = Instantiate(attackCollider, transform.position + (transform.forward * ATTACK_COLLIDER_INS_DIS) + offSet, transform.rotation);

        obj.GetComponent<MonsterAttackCollider>().damage = myStat.GetDamage(_EIntStatType_.eDamage);
        obj.GetComponent<MonsterAttackCollider>().destroyTime = attackColliderDestroyTime;
        if (obj.GetComponent<ForwardMoveObject>() != null)
        {
            Transform target = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.eSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("AttackedPos"))
                        target = col.transform;
                }
            }

            obj.GetComponent<ForwardMoveObject>().target = target;
            obj.GetComponent<ForwardMoveObject>().speed = ATTACK_COLLIDER_SPEED;
        }
    }

    protected override IEnumerator AfterAttack()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
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
                {
                    target = col.transform;
                    if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.eAttackRange))
                        stateManager.SetActionType(_EStateType_.eBattleIdle, _EObjectType_.eMonster);
                    else
                        stateManager.SetActionType(_EStateType_.eChase, _EObjectType_.eMonster);
                }
            }

            if (target == null)
                stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.eMonster);
        }
    }
}
