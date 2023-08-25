using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCollider : MonoBehaviour
{
    public int damage { get; set; }

    protected abstract void OnTriggerEnter(Collider other);
}
