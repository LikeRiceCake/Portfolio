using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void DoAction(_EStateType_ state)
    {
        base.DoAction(state);
        myAnim.ResetTrigger("Idle");
        myAnim.SetTrigger("Move");

        if (gameMgr.CheckMoveable())
        {
            StartCoroutine(InputMoveOn());

            StartCoroutine(InputMoveOff());

            StartCoroutine(InputSkill());

            StartCoroutine(InputDash());

            StartCoroutine(InputAttack());
        }

        StartCoroutine(MovePlayer());
    }

    public override void ExitState()
    {
        myAnim.ResetTrigger("Move");
    }
}
