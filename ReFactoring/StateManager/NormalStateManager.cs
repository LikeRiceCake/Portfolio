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
            ob.ReactNotify(_EObjectType_.eotNormal);
    }

    public override void AddObserver(IDieObserver _ob)
    {
        myDieObs.Add(_ob);
    }

    public override void RemoveObserver(IDieObserver _ob)
    {
        myDieObs.Remove(_ob);
    }

    public override void SetActionType(_EStateType_ _type, _EObjectType_ _dtype)
    {
        if (myState != null)
        {
            switch (currentState)
            {
                case _EStateType_.estIdle:
                case _EStateType_.estMove:
                case _EStateType_.estBattleIdle:
                case _EStateType_.estChase:
                    myState.ExitState();
                    break;
            }
            myState.StopAllCoroutines();
        }

        currentState = _type;

        Component[] temp = gameObject.GetComponents<State>();

        if (temp != null)
        {
            foreach (var state in temp)
                Destroy(state);
        }

        // Enter Çàµ¿
        switch (_type)
        {
            case _EStateType_.estIdle:
                myState = gameObject.AddComponent<NormalIdleState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estMove:
                myState = gameObject.AddComponent<NormalMoveState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estChase:
                myState = gameObject.AddComponent<NormalChaseState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estBattleIdle:
                myState = gameObject.AddComponent<NormalBattleIdleState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estAttack:
                myState = gameObject.AddComponent<NormalAttackState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estDie:
                myState = gameObject.AddComponent<NormalDieState>();
                myState.DoAction(_type);
                break;
        }
    }
}
