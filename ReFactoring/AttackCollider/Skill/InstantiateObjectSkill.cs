using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectSkill : MonoBehaviour
{
    ResourceManager resourceManager;

    GameObject instantiateObj;

    public int m_damage { get; set; }
    public float m_destroyTime { get; set; }

    public float m_insTime { get; set; }

    void Start()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
        instantiateObj = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlowUpEffect");

        Invoke("InstantiateSkill", m_insTime);
    }

    void InstantiateSkill()
    {
        GameObject effect = Instantiate(instantiateObj, transform.position, Quaternion.identity);
        effect.GetComponent<MonsterAttackCollider>().m_damage = m_damage;
        effect.GetComponent<SelfDestroyer>().m_destroyTime = m_destroyTime;
    }
}
