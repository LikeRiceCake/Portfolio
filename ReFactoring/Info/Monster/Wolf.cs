using PublicEnums.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Normal
{
    protected override void Awake()
    {
        myType = _EMonsterType_.eWolf;

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
        collectionInfo.name = "늑대";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Wolf");
        collectionInfo.description = "근접에서 물어뜯는 요괴이다.";
        collectionInfo.numbering = (int)_EMonsterType_.eWolf + 1;
        collectionInfo.skillDescription = "스킬 없음";
    }
}
