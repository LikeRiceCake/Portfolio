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
        collectionInfo.name = "��ȣ";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Heogho");
        collectionInfo.description = "â�Ϳ��� �������ϰ� �ִ� �Ŵ��� ��ȣ. ������ �ż��� �����ν� �α��� �꿡 ��� �־����� â�Ϳ� ���� ������ �������� �ִ�.";
        collectionInfo.numbering = (int)_EMonsterType_.eHeogho + 1;
        collectionInfo.skillDescription = "ȣ���� ���� ������ ������ ������.";
    }
}
