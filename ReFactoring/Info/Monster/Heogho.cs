using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums.State;

public class Heogho : Boss
{
    protected override void Awake()
    {
        myType = _EMonsterType_.eHeogho;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new HeoghoStat(GetComponent<BossStateManager>());

        base.Start();

        ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern_100>();
        ((HeoghoStat)stat).myBoss = this;
    }

    public void ChangePattern(_EHeoghoPattern_ pattern)
    {
        Destroy(GetComponent<MonsterPattern>());

        switch (pattern)
        {
            case _EHeoghoPattern_.e50:
                ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern_50>();
                break;
            case _EHeoghoPattern_.e30:
                ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern_30>();
                break;
        }
        
    }


    public override void InitCollectionInfo()
    {
        collectionInfo.name = "흑호";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Heogho");
        collectionInfo.description = "창귀에게 조종당하고 있는 거대한 흑호. 본래는 신성한 영물로써 인근의 산에 살고 있었으나 창귀에 의해 악행을 저지르고 있다.";
        collectionInfo.numbering = (int)_EMonsterType_.eHeogho + 1;
        collectionInfo.skillDescription = "호구와 같은 형태의 공격을 날린다.";
    }
}
