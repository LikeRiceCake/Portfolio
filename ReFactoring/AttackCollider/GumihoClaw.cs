using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoClaw : AttackCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            other.transform.GetComponent<IStat>().GetStat().UnderAttack(damage);
    }
}