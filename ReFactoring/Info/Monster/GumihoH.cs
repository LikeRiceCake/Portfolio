using PublicEnums.Monster;
using PublicEnums.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoH : Gumiho
{
    protected override void Awake()
    {
        myType = _EMonsterType_.eGumihoH;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new GumihoHStat(GetComponent<BossStateManager>());

        base.Start();

        gameObject.AddComponent<GumihoPattern_100>();
    }
}
