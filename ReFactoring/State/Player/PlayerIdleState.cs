using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        myAnim.SetTrigger("Idle");
        ResetDir();

        if (gameMgr.CheckMoveable())
        {
            StartCoroutine(InputMoveOn());

            StartCoroutine(InputSkill());

            StartCoroutine(InputDash());

            StartCoroutine(InputAttack());
        }
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Idle");
    }
}
