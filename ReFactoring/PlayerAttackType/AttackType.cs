using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums;
using PublicEnums.State;

public abstract class AttackType
{
    protected Animator myAnim;

    protected PlayerAttackCollider weapon;

    protected Stat myStat;

    public abstract AttackType Attack();
    
    public IEnumerator SettingDamage()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_1"))
                break;
            else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_2"))
                break;
            else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_3"))
                break;
            else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_1"))
                break;
            else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_2"))
                break;
            else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_3"))
                break;

            yield return null;
        }

        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_1"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_2"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage_Sec);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_3"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage_Thi);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_1"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage_For);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_2"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage_Fif);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_3"))
            weapon.m_damage = myStat.GetDamage(_EIntStatType_.eistDamage_Six);

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

        weapon.m_damage = 0;
    }

    public abstract _EPlayerAttackType_ GetAttackType();
}