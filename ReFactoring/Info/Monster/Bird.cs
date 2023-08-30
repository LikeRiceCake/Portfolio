using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums.State;

public class Bird : Normal
{
    Transform myAttackPos;

    protected override void Awake()
    {
        myType = _EMonsterType_.emtBird;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new BirdStat(GetComponent<StateManager>());

        base.Start();

    }

    public override void InitCollectionInfo()
    {
        collectionInfo.name = "��";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Bird");
        collectionInfo.description = "���Ÿ����� �����ϴ� �䱫�̴�.";
        collectionInfo.numbering = (int)_EMonsterType_.emtBird + 1;
        collectionInfo.skillDescription = "��ų ����";
    }
}
