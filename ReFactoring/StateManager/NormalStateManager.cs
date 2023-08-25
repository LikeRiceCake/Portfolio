using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStateManager : StateManager
{
    private void Awake()
    {
        myDieObs = new List<IDieObserver>();
    }

    public override void NotifyDeath()
    {
        foreach (var ob in myDieObs)
            ob.ReactNotify(_EObjectType_.eNormal);
    }

    public override void AddObserver(IDieObserver ob)
    {
        myDieObs.Add(ob);
    }

    public override void RemoveObserver(IDieObserver ob)
    {
        myDieObs.Remove(ob);
    }

    public override void SetActionType(_EStateType_ newState, _EObjectType_ type)
    {
        if (myState != null)
        {
            switch (currentState)
            {
                case _EStateType_.eIdle:
                case _EStateType_.eMove:
                case _EStateType_.eBattleIdle:
                case _EStateType_.eChase:
                    myState.ExitState();
                    break;
            }
            myState.StopAllCoroutines();
        }

        currentState = newState;

        Component[] temp = gameObject.GetComponents<State>();

        if (temp != null)
        {
            foreach (var state in temp)
                Destroy(state);
        }

        // Enter Çàµ¿
        switch (newState)
        {
            case _EStateType_.eIdle:
                myState = gameObject.AddComponent<NormalIdleState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eMove:
                myState = gameObject.AddComponent<NormalMoveState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eChase:
                myState = gameObject.AddComponent<NormalChaseState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eBattleIdle:
                myState = gameObject.AddComponent<NormalBattleIdleState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eAttack:
                myState = gameObject.AddComponent<NormalAttackState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eDie:
                myState = gameObject.AddComponent<NormalDieState>();
                myState.DoAction(newState);
                break;
        }
    }
}
