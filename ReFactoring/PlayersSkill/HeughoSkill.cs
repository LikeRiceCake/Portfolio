using System.Collections;
using System.Collections.Generic;
using PublicEnums;
using UnityEngine;

public class HeughoSkill : PlayerSkill
{
    public override void SkillFunction()
    {
        // ���� ��ų ���.
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
