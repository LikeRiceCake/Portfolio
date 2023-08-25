using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum _EHeughoSkill_30_
{
    eSwingRightHand_Horizontal,
    eSwingBothHand_Vertical,
    eHowling_RLAuraAttack,
    eSwingRLR_Horizontal,
    eHowling_BlackLightning,
    eBlackAura_Field,
    eMax
}

public class HeughoPattern_30 : HeughoPattern_50
{
    private void Start()
    {
        BlackAura_Field();
    }

    void BlackAura_Field()
    {
        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/BlackAura");

        GameObject obj = Instantiate(skillPrefab, transform);
        obj.transform.position = transform.position;
        obj.name = "Field_Aura";
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.eHeogho_30;
    }
}
