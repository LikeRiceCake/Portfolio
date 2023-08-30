using PublicEnums.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Normal
{
    protected override void Awake()
    {
        myType = _EMonsterType_.emtWolf;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new WolfStat(GetComponent<NormalStateManager>());

        base.Start();

        //myClips = new AudioClip[(int)_ENormalSoundType_.eMax];

        //myClips[(int)_ENormalSoundType_.eIdle] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Wolf Idle");
        //myClips[(int)_ENormalSoundType_.eAttack] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Wolf At");
    }

    public override void InitCollectionInfo()
    {
        collectionInfo.name = "����";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Wolf");
        collectionInfo.description = "�������� ������ �䱫�̴�.";
        collectionInfo.numbering = (int)_EMonsterType_.emtWolf + 1;
        collectionInfo.skillDescription = "��ų ����";
    }
}
