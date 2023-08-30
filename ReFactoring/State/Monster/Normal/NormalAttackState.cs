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
    float attackColliderm_destroyTime;

    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);

        ResourceManager resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        switch (GetComponent<Monster>().GetMonsterType())
        {
            case _EMonsterType_.emtWolf:
            case _EMonsterType_.emtBoar:
                attackCollider = resourceManager.LoadMonsterPrefab("Prefabs/SkillObject/CloseAttack");
                attackColliderm_destroyTime = 0.1f;
                break;
            case _EMonsterType_.emtBird:
                attackCollider = resourceManager.LoadMonsterPrefab("Prefabs/SkillObject/LongAttack");
                attackColliderm_destroyTime = 5f;
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

        obj.GetComponent<MonsterAttackCollider>().m_damage = myStat.GetDamage(_EIntStatType_.eistDamage);
        obj.GetComponent<MonsterAttackCollider>().m_destroyTime = attackColliderm_destroyTime;
        if (obj.GetComponent<ForwardMoveObject>() != null)
        {
            Transform target = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col.CompareTag("AttackedPos"))
                        target = col.transform;
                }
            }

            obj.GetComponent<ForwardMoveObject>().target = target;
            obj.GetComponent<ForwardMoveObject>().m_speed = ATTACK_COLLIDER_SPEED;
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
        _EFloatStatType_.efstCurrentAttackCool,
        myStat.GetFloatStat(_EFloatStatType_.efstAttackCool) + (myStat.GetFloatStat(_EFloatStatType_.efstCurrentAttackCool) * -1));

        Transform target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, myStat.GetFloatStat(_EFloatStatType_.efstSight));

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                {
                    target = col.transform;
                    if (Vector3.Distance(target.position, transform.position) <= myStat.GetFloatStat(_EFloatStatType_.efstAttackRange))
                        stateManager.SetActionType(_EStateType_.estBattleIdle, _EObjectType_.eotMonster);
                    else
                        stateManager.SetActionType(_EStateType_.estChase, _EObjectType_.eotMonster);
                }
            }

            if (target == null)
                stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotMonster);
        }
    }
}
