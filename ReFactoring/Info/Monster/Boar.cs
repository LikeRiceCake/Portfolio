using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;

public class Boar : Normal
{
    protected override void Awake()
    {
        myType = _EMonsterType_.eBoar;

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
        collectionInfo.name = "멧돼지";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Boar");
        collectionInfo.description = "근거리에서 공격하는 요괴이다.";
        collectionInfo.numbering = (int)_EMonsterType_.eBoar + 1;
        collectionInfo.skillDescription = "스킬 없음";
    }
}
