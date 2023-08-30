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

        AddSkill(_ESkillType_.estCrow);

        ChangeSkill(_ESkillType_.estCrow);
    }

    void ChangeSkill(_ESkillType_ _type)
    {
        currentSkill = _type;

        skillList.SetCurrentIndex = (int)currentSkill;

        SkillEnter();
    }

    void SkillEnter()
    {
        skillList.GetCurrent.ChangeImage();
    }

    void UseSkill()
    {
        skillList.GetCurrent.ActivateSkillFunction();
    }

    public void AddSkill(_ESkillType_ _type)
    {
        switch (_type)
        {
            case _ESkillType_.estCrow:
                skillList.Add(gameObject.AddComponent<CrowSkill>());
                break;
            case _ESkillType_.estGumiho:
                skillList.Add(gameObject.AddComponent<GumihoSkill>());
                break;
            case _ESkillType_.estHeogho:
                break;
        }
        
    }

    public void ReactNotify(_EInputType_ _type, _EInputDetailType_ _dType)
    {
        if(_type == _EInputType_.eitSkill)
        {
            switch (_dType)
            {
                case _EInputDetailType_.eidtChangeSkill:
                    skillList.Next();
                    SkillEnter();
                    break;
                case _EInputDetailType_.eidtUseSkill:
                    UseSkill();
                    break;
            }
        }
    }
}
