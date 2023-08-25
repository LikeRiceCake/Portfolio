using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : AttackCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            other.transform.GetComponent<IStat>().GetStat().UnderAttack(damage);
        }
    }
}