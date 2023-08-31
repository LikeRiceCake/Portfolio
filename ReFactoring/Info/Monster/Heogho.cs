using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums.State;

public class Heogho : Boss
{
    protected override void Awake()
    {
        myType = _EMonsterType_.emtHeogho;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new HeoghoStat(GetComponent<BossStateManager>());

        m_particleDamage = 3;

        base.Start();

        ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern100>();
        ((HeoghoStat)stat).myBoss = this;
    }

    // ChangePattern 구미호에도 만들어서(Boss에서 상속) 구미호는 변신(Scene 불러오는것) 흑호는 패턴만 변경
    public void ChangePattern(_EHeoghoPattern_ pattern)
    {
        Destroy(GetComponent<MonsterPattern>());

        switch (pattern)
        {
            case _EHeoghoPattern_.e50:
                ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern50>();
                break;
            case _EHeoghoPattern_.e30:
                ((HeoghoStat)stat).mp = gameObject.AddComponent<HeughoPattern30>();
                break;
        }
        
    }


    public override void InitCollectionInfo()
    {
        collectionInfo.name = "��ȣ";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Heogho");
        collectionInfo.description = "â�Ϳ��� �������ϰ� �ִ� �Ŵ��� ��ȣ. ������ �ż��� �����ν� �α��� �꿡 ��� �־����� â�Ϳ� ���� ������ �������� �ִ�.";
        collectionInfo.numbering = (int)_EMonsterType_.emtHeogho + 1;
        collectionInfo.skillDescription = "ȣ���� ���� ������ ������ ������.";
    }
}
