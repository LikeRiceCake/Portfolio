using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum _EHeughoSkill30_
{
    ehsSwingRightHand,
    ehsSwingBothHand,
    ehsHowling_RLAuraAttack,
    ehsSwingRLR_Horizontal,
    ehsHowling_BlackLightning,
    ehsActivateField,
    ehsMax
}

public class HeughoPattern30 : HeughoPattern50
{
    private void Start()
    {
        ActivateField();
    }

    void ActivateField()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlackAura");

        GameObject obj = Instantiate(skillPrefab, transform);
        obj.transform.position = transform.position;
        obj.name = "Field_Aura";
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.empHeogho30;
    }
}
