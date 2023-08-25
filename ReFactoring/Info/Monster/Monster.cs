using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicStructs.Encyclopedia;
using PublicEnums.State;
using PublicStructs.Character;
using PublicEnums;
using PublicEnums.Monster;

public abstract class Monster : MonoBehaviour, ICollectible, IStat
{
    ControlStrategy myControl;

    protected _EMonsterType_ myType;

    protected Stat stat;

    protected ResourceManager resourceManager;

    protected CollectionInfo collectionInfo;

    Rigidbody myBody;

    protected virtual void Awake()
    {
        myControl = gameObject.AddComponent<AIStrategy>();

        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        myBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        InitCollectionInfo();
        myControl.ControlAction();
    }

    private void Update()
    {
        // 밀림 방지
        myBody.velocity = Vector3.zero;
        myBody.angularVelocity = Vector3.zero;
    }

    public virtual void InitCollectionInfo()
    {
        collectionInfo.name = "오류";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Error");
        collectionInfo.description = "오류";
        collectionInfo.numbering = 0;
        collectionInfo.skillDescription = "오류";
    }

    public _EMonsterType_ GetMonsterType()
    {
        return myType;
    }

    public virtual Stat GetStat()
    {
        return stat;
    }
}
