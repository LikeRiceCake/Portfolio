using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums;
using PublicEnums.State;

public class Strong : AttackType
{
    public Strong(Animator _myAnim, PlayerAttackCollider _weapon, Stat _myStat)
    {
        base.myAnim = _myAnim;
        this.weapon = _weapon;
        this.myStat = _myStat;
    }

    public override AttackType Attack()
    {
        myAnim.SetTrigger("Strong");
        
        return this;
    }

    public override _EPlayerAttackType_ GetAttackType()
    {
        return _EPlayerAttackType_.epatStrong;
    }
}