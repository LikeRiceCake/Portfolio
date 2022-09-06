using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPattern : MonoBehaviour
{
    public Transform Player;
    public GameObject Enemy;
    public EnemyMove em;
    public Animator anim;
    public Switchboard sb;

    public float Delay;
    public float chasingTime;
    public float chasingTimeSet;

    public bool isActive;

    void Awake()
    {
        Enemy.gameObject.SetActive(false);
        Delay = 30f;
        chasingTimeSet = 15f;
        chasingTime = chasingTimeSet;
        isActive = false;
        anim = Enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        delay();
        chase();
        if (sb.switchBoard)
        {
            chasingTimeSet = 20f;
        }
    }

    public void TouchItem()
    {
        if (!isActive)
        {
            Enemy.gameObject.SetActive(true);
            isActive = true;
            Delay = 30f;
        }
        Enemy.transform.position = new Vector3(Player.position.x + Random.Range(-10f, 10f), Player.position.y, Player.position.z + Random.Range(-10f, 10f));
    }

    public void delay()
    {
        if (!isActive)
        {
            Delay -= Time.deltaTime;
            if (Delay <= 0f)
            {
                Enemy.gameObject.SetActive(true);
                Enemy.transform.position = new Vector3(Player.position.x + Random.Range(-15, 15), Player.position.y, Player.position.z + Random.Range(-15, 15));
                isActive = true;
                Delay = 30f;
            }
        }
    }

    public void chase()
    {
        if (isActive)
        {
            if (em.isSee)
            {
                chasingTime -= Time.deltaTime;
            }
            if (chasingTime <= 0f)
            {
                Enemy.gameObject.SetActive(false);
                isActive = false;
                em.isSee = false;
                anim.SetTrigger("isTimeOut");
                em.checkviewTime = 0f;
                chasingTime = chasingTimeSet;
            }
        }
    }
}
