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
        myType = _EMonsterType_.eBird;

        base.Awake();
    }

    protected override void Start()
    {
        stat = new BirdStat(GetComponent<StateManager>());

        base.Start();

        //myClips = new AudioClip[(int)_ENormalSoundType_.eMax];

        //myClips[(int)_ENormalSoundType_.eIdle] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Bird Idle");
        //myClips[(int)_ENormalSoundType_.eAttack] = resourceManager.LoadAudioClip("Sounds/SFX/Normal/Bird At");
        //attackCollider = resourceManager.LoadSkillPrefab("Prefabs/Colliders/LongAttack");
    }

    public override void InitCollectionInfo()
    {
        collectionInfo.name = "새";
        collectionInfo.sprite = resourceManager.LoadMonsterSprite("Sprite/Bird");
        collectionInfo.description = "원거리에서 공격하는 요괴이다.";
        collectionInfo.numbering = (int)_EMonsterType_.eBird + 1;
        collectionInfo.skillDescription = "스킬 없음";
    }
}
