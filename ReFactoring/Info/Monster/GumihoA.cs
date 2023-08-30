using PublicEnums.Monster;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoA : Gumiho
{
    protected override void Awake()
    {
        myType = _EMonsterType_.emtGumihoA;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new GumihoAStat(GetComponent<BossStateManager>());

        base.Start();

        gameObject.AddComponent<GumihoPattern70>();
    }
}
