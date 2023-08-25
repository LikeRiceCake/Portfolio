using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour, IInputKeyClickObserver
{
    CircularList<PlayerSkill> skillList;

    _ESkillType_ currentSkill;

    private void Start()
    {
        GameObject.Find("Player").GetComponent<IInputKeyClickSubject>().AddObserver(this);

        skillList = new CircularList<PlayerSkill>();

        PlayerGetSkill(_ESkillType_.eCrow);

        ChangeSkill(_ESkillType_.eCrow);
    }

    void ChangeSkill(_ESkillType_ type)
    {
        currentSkill = type;

        skillList.SetCurrentIndex = (int)currentSkill;

        SkillEnter();
    }

    void SkillEnter()
    {
        skillList.GetCurrent.ChangeImage();
    }

    void UseSkill()
    {
        skillList.GetCurrent.SkillFunction();
    }

    public void PlayerGetSkill(_ESkillType_ type)
    {
        switch (type)
        {
            case _ESkillType_.eCrow:
                skillList.Add(gameObject.AddComponent<CrowSkill>());
                break;
            case _ESkillType_.eGumiho:
                skillList.Add(gameObject.AddComponent<GumihoSkill>());
                break;
            case _ESkillType_.eHeogho:
                break;
        }
        
    }

    public void ReactNotify(_EInputType_ type, _EInputDetailType_ dType)
    {
        if(type == _EInputType_.eSkill)
        {
            switch (dType)
            {
                case _EInputDetailType_.eChangeSkill:
                    skillList.Next();
                    SkillEnter();
                    break;
                case _EInputDetailType_.eUseSkill:
                    UseSkill();
                    break;
            }
        }
    }
}
