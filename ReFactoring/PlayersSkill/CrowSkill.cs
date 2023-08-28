using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using PublicEnums.UI.HP;
using UnityEngine;

public class CrowSkill : PlayerSkill
{
    Stat stat;

    CharacterHPUI hpUI;

    const int HEAL_VALUE = 70;

    const float HEAL_COOLTIME = 17f;

    const float EFFECT_DESTROY_TIME = 3f;
    
    public override void ActivateSkillFunction()
    {
        if(m_currentCoolTime <= 0)
        {
            base.ActivateSkillFunction();

            stat.PlusHp(_EIntStatType_.eistCurrentHp, HEAL_VALUE);

            hpUI.ChangeCharacterHPUI(
                _EHPUIType_.ehutPlayer,
                stat.GetIntStat(_EIntStatType_.eistCurrentHp),
                stat.GetIntStat(_EIntStatType_.eistMaxHp));

            GameObject effect = Instantiate(resourceManager.LoadSkillPrefab("Prefabs/HealingAura"), transform);

            Destroy(effect, EFFECT_DESTROY_TIME);

            m_currentCoolTime = HEAL_COOLTIME;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (skillImage.sprite.name == "HealSkillSprite")
            coolImage.fillAmount = m_currentCoolTime / HEAL_COOLTIME;
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

        m_currentCoolTime = 0f;
    }
}
