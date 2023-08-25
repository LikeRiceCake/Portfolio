using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using UnityEngine;

public class GumihoSkill : PlayerSkill
{
    GameObject spawnPos;
    GameObject spawnObject;

    public override void SkillFunction()
    {
        if (currentCoolTime <= 0)
        {
            base.SkillFunction();
            Instantiate(spawnObject, spawnPos.transform.position, Camera.main.transform.rotation).GetComponent<Will_O_The_Wisp_Player>().destroyTime = COOLTIME;
            currentCoolTime = COOLTIME;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        spawnObject = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Shot_WILL_O_THE_WISP_Player");
        mySprite = resourceManager.LoadSkillSprite("Sprite/Skill/GumihoSkillSprite");

        spawnPos = transform.Find("Tam").gameObject;
    }

    protected override void Update()
    {
        base.Update();
        if (skillUIImage.sprite.name == "GumihoSkillSprite")
            coolImage.fillAmount = currentCoolTime / COOLTIME;
    }

    void Start()
    {
        skillValue = 30;

        COOLTIME = 5f;

        currentCoolTime = 0f;
    }
}
