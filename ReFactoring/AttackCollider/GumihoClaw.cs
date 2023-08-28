using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoClaw : AttackCollider
{
    protected override void OnTriggerEnter(Collider _other)
    {
        if (_other.transform.CompareTag("Player"))
            _other.transform.GetComponent<IStat>().GetStat().UnderAttack(m_damage);
    }
}