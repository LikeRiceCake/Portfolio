using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : StateManager
{
    private void Awake()
    {
        myDieObs = new List<IDieObserver>();
    }

    public override void NotifyDeath()
    {
        foreach (var ob in myDieObs)
            ob.ReactNotify(_EObjectType_.eotBoss);
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
                myState = gameObject.AddComponent<BossIdleState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estMove:
                myState = gameObject.AddComponent<BossMoveState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estChase:
                myState = gameObject.AddComponent<BossChaseState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estBattleIdle:
                myState = gameObject.AddComponent<BossBattleIdleState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estAttack:
                myState = gameObject.AddComponent<BossAttackState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estDie:
                myState = gameObject.AddComponent<BossDieState>();
                myState.DoAction(_type);
                break;
        }
    }
}
