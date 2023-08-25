using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum _EHeughoSkill_50_
{
    eSwingRightHand_Horizontal,
    eSwingBothHand_Vertical,
    eHowling_RLAuraAttack,
    eSwingRLR_Horizontal,
    eHowling_BlackLightning,
    eMax
}
public class HeughoPattern_50 : HeughoPattern_100
{
    public object Howlingeffect;

    const string SWING_RLR_HORIZONTAL = "RLRAttack";
    const string HOWLING_BLACKLIGHTNING_PLAYERPLACE = "Howling_LR_Lightning";

    const int LIGHTNING_DAMAGE = 30;
    const int LIGHTNING_CNT = 30;
    const float LIGHTNING_DESTROY_TIME = 0.5f;
    const float LIGHTNING_INS_TIME = 0.7f;

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EHeughoSkill_50_.eSwingRightHand_Horizontal, (int)_EHeughoSkill_50_.eMax);

        switch (rand)
        {
            case (int)_EHeughoSkill_50_.eSwingRightHand_Horizontal:
                return SwingRightHand_Horizontal();
            case (int)_EHeughoSkill_50_.eSwingBothHand_Vertical:
                return SwingBothHand_Vertical();
            case (int)_EHeughoSkill_50_.eHowling_RLAuraAttack:
                return Howling_LR_Aura_Attack();
            case (int)_EHeughoSkill_50_.eSwingRLR_Horizontal:
                return SwingRLR_Horizontal();
            case (int)_EHeughoSkill_50_.eHowling_BlackLightning:
                return Howling_BlackLightning_PlayerPlace();
        }

        return null;
    }

    protected string SwingRLR_Horizontal()
    {
        myAnim.SetTrigger(SWING_RLR_HORIZONTAL);

        return SWING_RLR_HORIZONTAL;
    }
    
    protected string Howling_BlackLightning_PlayerPlace()
    {
        myAnim.SetTrigger(HOWLING_BLACKLIGHTNING_PLAYERPLACE);

        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Lightning Strike");

        StartCoroutine(InstantiateLightning());

        return HOWLING_BLACKLIGHTNING_PLAYERPLACE;
    }

    public IEnumerator InstantiateLightning()
    {
        Transform map = GameObject.Find("Map").transform;

        float randXMin = map.position.x - (map.localScale.x / 2);
        float randXMax = map.position.x + (map.localScale.x / 2);
        float randZMin = map.position.z - (map.localScale.z / 2);
        float randZMax = map.position.z + (map.localScale.z / 2);

        for (int i = 0; i < LIGHTNING_CNT; i++)
        {
            Vector3 instantiatePos = new Vector3(Random.Range(randXMin, randXMax), map.position.y, Random.Range(randZMin, randZMax));
            GameObject obj = Instantiate(skillPrefab, instantiatePos, Quaternion.identity);
            obj.GetComponent<MonsterAttackCollider>().damage = LIGHTNING_DAMAGE;
            obj.GetComponent<MonsterAttackCollider>().destroyTime = LIGHTNING_DESTROY_TIME;
            yield return new WaitForSeconds(LIGHTNING_INS_TIME);
        }
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.eHeogho_50;
    }
}
