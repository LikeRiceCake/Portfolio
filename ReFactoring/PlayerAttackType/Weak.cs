using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums;
using PublicEnums.State;

public class Weak : AttackType
{
    public Weak(Animator myAnim, PlayerAttackCollider weapon, Stat myStat)
    {
        base.myAnim = myAnim;
        this.weapon = weapon;
        this.myStat = myStat;
    }

    public override AttackType Attack()
    {
        myAnim.SetTrigger("Weak");

        return this;
    }

    public override _EPlayerAttackType_ GetAttackType()
    {
        return _EPlayerAttackType_.eWeak;
    }
}
