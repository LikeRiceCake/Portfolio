using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using UnityEngine;

public class GumihoSkill : PlayerSkill
{
    GameObject spawnPos;
    GameObject spawnObject;

    const int DAMAGE = 30;

    const float COOLTIME = 7f;

    public override void ActivateSkillFunction()
    {
        if (m_currentCoolTime <= 0)
        {
            base.ActivateSkillFunction();
            Instantiate(spawnObject, spawnPos.transform.position, Camera.main.transform.rotation).GetComponent<Will_O_The_Wisp_Player>().m_destroyTime = COOLTIME;
            // 데미지 설정 필요
            m_currentCoolTime = COOLTIME;
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
        if (skillImage.sprite.name == "GumihoSkillSprite")
            coolImage.fillAmount = m_currentCoolTime / COOLTIME;
    }

    void Start()
    {
        m_currentCoolTime = 0f;
    }
}
