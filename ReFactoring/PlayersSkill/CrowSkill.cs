using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using PublicEnums.UI.HP;
using UnityEngine;

public class CrowSkill : PlayerSkill
{
    Stat stat;

    CharacterHPUI hpUI;

    const float EFFECT_DESTROY_TIME = 3f;
    
    public override void SkillFunction()
    {
        if(currentCoolTime <= 0)
        {
            base.SkillFunction();

            stat.PlusHp(_EIntStatType_.eCurrentHp, skillValue);

            hpUI.ChangeCharacterHPUI(
                _EHPUIType_.ePlayer,
                stat.GetIntStat(_EIntStatType_.eCurrentHp),
                stat.GetIntStat(_EIntStatType_.eMaxHp));

            GameObject effect = Instantiate(resourceManager.LoadSkillPrefab("Prefabs/HealingAura"), transform);

            Destroy(effect, EFFECT_DESTROY_TIME);

            currentCoolTime = COOLTIME;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (skillUIImage.sprite.name == "HealSkillSprite")
            coolImage.fillAmount = currentCoolTime / COOLTIME;
    }

    protected override void Awake()
    {
        base.Awake();

        hpUI = GameObject.Find("CharacterHP").GetComponent<CharacterHPUI>();

        mySprite = resourceManager.LoadSkillSprite("Sprite/Skill/HealSkillSprite");
    }

    void Start()
    {
        stat = GetComponent<IStat>().GetStat();

        skillValue = 70;

        COOLTIME = 17f;

        currentCoolTime = 0f;
    }
}
