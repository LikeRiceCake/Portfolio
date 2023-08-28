using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

enum _EHeughoSkill100_
{
    ehsSwingRightHandHorizontal,
    ehsSwingBothHandVertical,
    ehsHowlingLRAuraAttack,
    ehsMax
}

public class HeughoPattern100 : MonsterPattern
{ 
    const int AURA_DAMAGE = 20;

    const string SWING_RIGHTHAND_HORIZONTAL_ANIM_NAME = "RightAttack";
    const string SWING_BOTHHAND_VERTICAL_ANIM_NAME = "BothAttack";
    const string HowlingLRAura_ANIM_NAME = "HowlingLR";

    private void Start()
    {
        transform.Find("howlingC").Find("shockwave_right").Find("shockwave_right2").GetComponent<ParticleSkill>().m_damage = transform.Find("howlingC").Find("shockwave_left").Find("shockwave_left2").GetComponent<ParticleSkill>().m_damage = AURA_DAMAGE;
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EHeughoSkill100_.ehsSwingRightHandHorizontal, (int)_EHeughoSkill100_.ehsMax);

        switch (rand)
        {
            case (int)_EHeughoSkill100_.ehsSwingRightHandHorizontal:
                return SwingRightHand();
            case (int)_EHeughoSkill100_.ehsSwingBothHandVertical:
                return SwingBothHand();
            case (int)_EHeughoSkill100_.ehsHowlingLRAuraAttack:
                return HowlingLRAura();
        }

        return null;
    }


    protected string SwingRightHand()
    {
        myAnim.SetTrigger(SWING_RIGHTHAND_HORIZONTAL_ANIM_NAME);

        return SWING_RIGHTHAND_HORIZONTAL_ANIM_NAME;
    }

    protected string SwingBothHand()
    {
        myAnim.SetTrigger(SWING_BOTHHAND_VERTICAL_ANIM_NAME);

        return SWING_BOTHHAND_VERTICAL_ANIM_NAME;
    }

    protected string HowlingLRAura()
    {
        myAnim.SetTrigger(HowlingLRAura_ANIM_NAME);

        return HowlingLRAura_ANIM_NAME;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.empHeogho100;
    }
}