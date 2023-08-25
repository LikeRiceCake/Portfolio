using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill : MonoBehaviour
{
    protected int Attack;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //collision.transform.GetComponent<IDamaged>().GetDamaged(Attack);
            Destroy(gameObject);
        }
    }
}