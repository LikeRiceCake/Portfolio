using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

enum _EHeughoSkill_100_
{
    eSwingRightHand_Horizontal,
    eSwingBothHand_Vertical,
    eHowling_LRAuraAttack,
    eMax
}

public class HeughoPattern_100 : MonsterPattern
{ 
    const int AURA_DAMAGE = 20;

    const string SWING_RIGHTHAND_HORIZONTAL = "RightAttack";
    const string SWING_BOTHHAND_VERTICAL = "BothAttack";
    const string HOWLING_LR_AURA_ATTACK = "HowlingLR";

    private void Start()
    {
        transform.Find("howlingC").Find("shockwave_right").Find("shockwave_right2").GetComponent<ParticleSkill>().damage = transform.Find("howlingC").Find("shockwave_left").Find("shockwave_left2").GetComponent<ParticleSkill>().damage = AURA_DAMAGE;
    }

    public override string SetRandomSkill()
    {
        int rand = Random.Range((int)_EHeughoSkill_100_.eSwingRightHand_Horizontal, (int)_EHeughoSkill_100_.eMax);

        switch (rand)
        {
            case (int)_EHeughoSkill_100_.eSwingRightHand_Horizontal:
                return SwingRightHand_Horizontal();
            case (int)_EHeughoSkill_100_.eSwingBothHand_Vertical:
                return SwingBothHand_Vertical();
            case (int)_EHeughoSkill_100_.eHowling_LRAuraAttack:
                return Howling_LR_Aura_Attack();
        }

        return null;
    }


    protected string SwingRightHand_Horizontal()
    {
        myAnim.SetTrigger(SWING_RIGHTHAND_HORIZONTAL);

        return SWING_RIGHTHAND_HORIZONTAL;
    }

    protected string SwingBothHand_Vertical()
    {
        myAnim.SetTrigger(SWING_BOTHHAND_VERTICAL);

        return SWING_BOTHHAND_VERTICAL;
    }

    protected string Howling_LR_Aura_Attack()
    {
        myAnim.SetTrigger(HOWLING_LR_AURA_ATTACK);

        return HOWLING_LR_AURA_ATTACK;
    }

    public override _EMonsterPattern_ GetNowPattern()
    {
        return _EMonsterPattern_.eHeogho_100;
    }
}