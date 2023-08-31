using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCollider : AttackCollider
{
    protected override void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Monster"))
        {
            _other.GetComponent<IStat>().GetStat().UnderAttack(m_damage);
            Destroy(gameObject);
        }
    }
}
