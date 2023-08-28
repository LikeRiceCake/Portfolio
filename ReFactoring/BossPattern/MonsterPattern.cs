using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _EMonsterPattern_
{
    empGumiho100,
    empGumiho70,
    empHeogho100,
    empHeogho50,
    empHeogho30,
    empMax
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
