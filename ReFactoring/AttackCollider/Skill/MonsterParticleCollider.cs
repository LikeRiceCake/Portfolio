using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleCollider : ParticleCollider
{
    protected override void OnParticleCollision(GameObject _other)
    {
        if(_other.CompareTag("Player"))
            _other.GetComponent<IStat>().GetStat().UnderAttack(m_damage);
    }
}
