using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Aura : FixedSkill
{
    float currentTime;

    const float SECOND = 3f;

    //protected override void Start()
    //{
    //    Attack = 30;
    //}

    private void Update()
    {
        BehaviourFunc();
    }

    protected override void BehaviourFunc()
    {
        if(currentTime > 0)
            currentTime -= Time.deltaTime;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(currentTime <= 0)
            {
                //collision.transform.GetComponent<IDamaged>().GetDamaged(Attack);
                currentTime = SECOND;
            }
        }
    }
}
