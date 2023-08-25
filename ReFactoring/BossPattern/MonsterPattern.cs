using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _EMonsterPattern_
{
    eGumiho_100,
    eGumiho_70,
    eHeogho_100,
    eHeogho_50,
    eHeogho_30,
    eMax
}

public abstract class MonsterPattern : MonoBehaviour
{
    protected ResourceManager resourceManager;

    protected GameObject skillPrefab;

    protected Animator myAnim;

    protected Stat myStat;

    void Awake()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
        myAnim = GetComponent<Animator>();
        myStat = GetComponent<IStat>().GetStat();
    }

    public abstract string SetRandomSkill();

    public abstract _EMonsterPattern_ GetNowPattern();
}
