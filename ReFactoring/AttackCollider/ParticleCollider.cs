using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleCollider : MonoBehaviour
{
    public int m_damage { get; set; }

    protected abstract void OnParticleCollision(GameObject _other);
}
