using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.State;

public class PlayerDieState : PlayerState
{
    public override void DoAction(_EStateType_ _type)
    {
        myAnim.SetTrigger("Die");

        StartCoroutine(CallDie());
    }

    IEnumerator CallDie()
    {
        while(true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

        stateManager.NotifyDeath();
    }
}
