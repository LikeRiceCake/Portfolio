using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;

public class Boar : Normal
{
    protected override void Awake()
    {
        myType = _EMonsterType_.emtBoar;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new BoarStat(GetComponent<NormalStateManager>());

        base.Start();

        //myClips = new AudioClip[(int)_ENormalSoundType_.eMax];

        //myClips[(int)_ENormalSoundType_.eIdle] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Boar Idle");
        //myClips[(int)_ENormalSoundType_.eAttack] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Boar At");
    }

    public override void InitCollectionInfo()
    {
        collectionInfo.name = "�����";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Boar");
        collectionInfo.description = "�ٰŸ����� �����ϴ� �䱫�̴�.";
        collectionInfo.numbering = (int)_EMonsterType_.emtBoar + 1;
        collectionInfo.skillDescription = "��ų ����";
    }
}
