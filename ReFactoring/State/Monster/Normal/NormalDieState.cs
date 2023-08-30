using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDieState : MonsterDieState
{
    public override void DoAction(_EStateType_ _type)
    {
        base.DoAction(_type);

        StartCoroutine(AfterDie());
    }

    IEnumerator AfterDie()
    {
        while (true)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
                break;

            yield return null;
        }

        yield return new WaitUntil(() => myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        stateManager.NotifyDeath();

        Destroy(gameObject);
    }
}