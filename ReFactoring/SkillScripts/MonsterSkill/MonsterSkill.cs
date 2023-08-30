using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill : MonoBehaviour
{
    protected int m_attack;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //collision.transform.GetComponent<IDamaged>().GetDamaged(m_attack);
            Destroy(gameObject);
        }
    }
}