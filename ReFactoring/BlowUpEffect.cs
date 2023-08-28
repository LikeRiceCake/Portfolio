using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpEffect : MonoBehaviour
{
    [SerializeField]
    int m_damage;

    void Start()
    {
        m_damage = 40;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //other.transform.GetComponent<IDamaged>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }
}
