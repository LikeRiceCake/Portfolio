using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ChasingSkill : MonsterSkill
{
    public Transform target { get; set; }
    GameObject FireballEffect;
    ResourceManager resourceManager;
    const float SPEED = 10f;

    [SerializeField]
    int damage;

    void Start()
    {
        damage = GameObject.Find("Gumiho_H(Clone)") != null ? 20 : 30;

        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
        FireballEffect = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Fireball");
    }

    public void SetLook()
    {
        transform.LookAt(target.position);
    }

    void Update()
    {
        if (target != null)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
            Destroy(gameObject, 2f);
        }
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
