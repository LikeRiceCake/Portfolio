using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : AttackCollider
{
    public float destroyTime { get; set; }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<IStat>().GetStat().UnderAttack(damage);
            Destroy(gameObject);
        }
    }
}
