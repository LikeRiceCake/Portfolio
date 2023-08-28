using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectSkill : MonoBehaviour
{
    ResourceManager resourceManager;
    GameObject blowUpEffect;

    public int damage { get; set; }
    public float m_destroyTime { get; set; }

    public float insTime { get; set; }

    void Start()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
        blowUpEffect = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlowUpEffect");

        Invoke("InstantiateSkill", insTime);
    }

    void InstantiateSkill()
    {
        GameObject effect = Instantiate(blowUpEffect, transform.position, Quaternion.identity);
        effect.GetComponent<MonsterAttackCollider>().m_damage = damage;
        effect.GetComponent<MonsterAttackCollider>().m_destroyTime = m_destroyTime;
        Destroy(gameObject);
    }
}
