using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using UnityEngine;

public class HeughoSkill : PlayerSkill
{
    public override void SkillFunction()
    {
        // 각각 스킬 기능.
    }

    public override void ChangeImage()
    {
        base.ChangeImage();
    }

    void Start()
    {
        mySprite = resourceManager.LoadSkillSprite("Sprite/Skill/Heugho");
    }
}
