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
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_2"))
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage_Sec);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("W_Attack_3"))
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage_Thi);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_1"))
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage_For);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_2"))
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage_Fif);
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("S_Attack_3"))
            weapon.damage = myStat.GetDamage(_EIntStatType_.eDamage_Six);

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

        weapon.damage = 0;
    }

    public abstract _EPlayerAttackType_ GetAttackType();
}