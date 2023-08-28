using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Animator myAnim;
    protected StateManager stateManager;
    protected Stat myStat;

    protected virtual void Awake()
    {
        myAnim = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
        myStat = GetComponent<IStat>().GetStat();
    }

    public abstract void DoAction(_EStateType_ _type);

    public virtual void ExitState()
    {
        myAnim.ResetTrigger("");
    }
}