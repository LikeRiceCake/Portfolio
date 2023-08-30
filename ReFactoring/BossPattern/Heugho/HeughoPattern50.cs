using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum _EHeughoSkill50_
{
    ehsSwingRightHandHorizontal,
    ehsSwingBothHandVertical,
    ehsHowlingRLAuraAttack,
    ehsSwingRLRHorizontal,
    ehsHowlingBlackLightning,
    ehsMax
}
public class HeughoPattern50 : HeughoPattern100
{
    public object Howlingeffect;

    const string SWING_RLR_HORIZONTAL_ANIM_NAME = "RLRAttack";
    const string HOWLING_BLACKLIGHTNING_PLAYERPLACE_ANIM_NAME = "Howling_LR_Lightning";

    const int LIGHTNING_DAMAGE = 30;
    const int LIGHTNING_INS_NUM = 30;
    const float LIGHTNING_DESTROY_TIME = 0.5f;
    const float LIGHTNING_INS_TIME = 0.7f;

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EHeughoSkill50_.ehsSwingRightHandHorizontal, (int)_EHeughoSkill50_.ehsMax);

        switch (rand)
        {
            case (int)_EHeughoSkill50_.ehsSwingRightHandHorizontal:
                return SwingRightHand();
            case (int)_EHeughoSkill50_.ehsSwingBothHandVertical:
                return SwingBothHand();
            case (int)_EHeughoSkill50_.ehsHowlingRLAuraAttack:
                return HowlingLRAura();
            case (int)_EHeughoSkill50_.ehsSwingRLRHorizontal:
                return SwingRLR();
            case (int)_EHeughoSkill50_.ehsHowlingBlackLightning:
                return HowlingLightning();
        }

        return null;
    }

    protected string SwingRLR()
    {
        myAnim.SetTrigger(SWING_RLR_HORIZONTAL_ANIM_NAME);

        return SWING_RLR_HORIZONTAL_ANIM_NAME;
    }
    
    protected string HowlingLightning()
    {
        myAnim.SetTrigger(HOWLING_BLACKLIGHTNING_PLAYERPLACE_ANIM_NAME);

        skillPrefab = resourceManager.LoadSkillPrefab("Prefabs/SkillObject/Lightning Strike");

        StartCoroutine(InstantiateLightning());

        return HOWLING_BLACKLIGHTNING_PLAYERPLACE_ANIM_NAME;
    }

    public IEnumerator InstantiateLightning()
    {
        Transform map = GameObject.Find("Map").transform;

        float randXMin = map.position.x - (map.localScale.x / 2);
        float randXMax = map.position.x + (map.localScale.x / 2);
        float randZMin = map.position.z - (map.localScale.z / 2);
        float randZMax = map.position.z + (map.localScale.z / 2);

        for (int i = 0; i < LIGHTNING_INS_NUM; i++)
        {
            Vector3 instantiatePos = new Vector3(Random.Range(randXMin, randXMax), map.position.y, Random.Range(randZMin, randZMax));
            GameObject obj = Instantiate(skillPrefab, instantiatePos, Quaternion.identity);
            obj.GetComponent<MonsterAttackCollider>().m_damage = LIGHTNING_DAMAGE;
            obj.GetComponent<MonsterAttackCollider>().m_destroyTime = LIGHTNING_DESTROY_TIME;
            yield return new WaitForSeconds(LIGHTNING_INS_TIME);
        }
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.empHeogho50;
    }
}
