using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerStateManager : StateManager, IInputKeyClickSubject, IInputMouseRotateSubject, IDieSubject
{
    List<IInputKeyClickObserver> myKeyObs = new List<IInputKeyClickObserver>();
    List<IInputMouseRotateObserver> myMouseRotateObs = new List<IInputMouseRotateObserver>();

    public void AddObserver(IInputKeyClickObserver ob)
    {
        myKeyObs.Add(ob);
    }

    public void RemoveObserver(IInputKeyClickObserver ob)
    {
        myKeyObs.Remove(ob);
    }

    public void NotifyKeyClickInput(_EInputType_ type, _EInputDetailType_ dType)
    {
        foreach (var ob in myKeyObs)
            ob.ReactNotify(type, dType);
    }

    public void NotifyMouseRotate(_EInputType_ type, _EInputDetailType_ dType, Vector2 value)
    {
        foreach (var ob in myMouseRotateObs)
            ob.ReactNotify(type, dType, value);
    }

    public void AddObserver(IInputMouseRotateObserver ob)
    { 
        myMouseRotateObs.Add(ob);
    }

    public void RemoveObserver(IInputMouseRotateObserver ob)
    {
        myMouseRotateObs.Remove(ob);
    }

    public override void NotifyDeath()
    {
        foreach (var ob in myDieObs)
            ob.ReactNotify(_EObjectType_.ePlayer);
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
        // Exit 행동
        if (myState != null)
        {
            switch (currentState)
            {
                case _EStateType_.eIdle:
                case _EStateType_.eMove:
                case _EStateType_.eSkill:
                case _EStateType_.eDash:
                case _EStateType_.eAttack:
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

        // Enter 행동
        switch (newState)
        {
            case _EStateType_.eIdle:
                myState = gameObject.AddComponent<PlayerIdleState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eMove:
                myState = gameObject.AddComponent<PlayerMoveState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eDash:
                myState = gameObject.AddComponent<PlayerDashState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eAttack:
                myState = gameObject.AddComponent<PlayerAttackState>();
                myState.DoAction(newState);
                break;
            case _EStateType_.eDie:
                myState = gameObject.AddComponent<PlayerDieState>();
                myState.DoAction(newState);
                break;
        }
    }
}
