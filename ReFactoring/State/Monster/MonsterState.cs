using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState : State
{
    const float GRAVITY = 0.98f;

    public override void DoAction(_EStateType_ _type)
    {
        StartCoroutine(ApplyGravity());
    }

    protected virtual IEnumerator ApplyGravity()
    {
        while (true)
        {
            transform.Translate(Vector3.down * Time.deltaTime * GRAVITY);

            yield return null;
        }
    }
}
