using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : AttackCollider
{
    public float m_destroyTime { get; set; }

    protected virtual void Start()
    {
        Destroy(gameObject, m_destroyTime);
    }

    protected override void OnTriggerEnter(Collider _other)
    {
        if(_other.transform.CompareTag("Player"))
        {
            _other.transform.GetComponent<IStat>().GetStat().UnderAttack(m_damage);
            Destroy(gameObject);
        }
    }
}
