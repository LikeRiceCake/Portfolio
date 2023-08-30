using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCollider : MonoBehaviour
{
    // AttackCollider 관련 클래스 재정비 필요

    public int m_damage { get; set; }

    protected abstract void OnTriggerEnter(Collider _other);
}
