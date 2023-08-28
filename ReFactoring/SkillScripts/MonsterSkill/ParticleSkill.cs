using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSkill : MonoBehaviour
{
    public int m_damage { get; set; }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"LayerMask.NameToLayer(\"Player\") : {LayerMask.NameToLayer("Player")}, other.gameObject.layer : {other.gameObject.layer}, object : {other}");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log($"other ¡¯¿‘ : {other}");
            other.GetComponent<IStat>().GetStat().UnderAttack(m_damage);
            Debug.Log(other);
        }
    }
}
