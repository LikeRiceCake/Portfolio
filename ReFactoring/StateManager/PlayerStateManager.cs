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

    public void AddObserver(IInputKeyClickObserver _ob)
    {
        myKeyObs.Add(_ob);
    }

    public void RemoveObserver(IInputKeyClickObserver _ob)
    {
        myKeyObs.Remove(_ob);
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

    public void AddObserver(IInputMouseRotateObserver _ob)
    { 
        myMouseRotateObs.Add(_ob);
    }

    public void RemoveObserver(IInputMouseRotateObserver _ob)
    {
        myMouseRotateObs.Remove(_ob);
    }

    public override void NotifyDeath()
    {
        foreach (var ob in myDieObs)
            ob.ReactNotify(_EObjectType_.eotPlayer);
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
        // Exit 행동
        if (myState != null)
        {
            switch (currentState)
            {
                case _EStateType_.estIdle:
                case _EStateType_.estMove:
                case _EStateType_.estSkill:
                case _EStateType_.estDash:
                case _EStateType_.estAttack:
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

        // Enter 행동
        switch (_type)
        {
            case _EStateType_.estIdle:
                myState = gameObject.AddComponent<PlayerIdleState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estMove:
                myState = gameObject.AddComponent<PlayerMoveState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estDash:
                myState = gameObject.AddComponent<PlayerDashState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estAttack:
                myState = gameObject.AddComponent<PlayerAttackState>();
                myState.DoAction(_type);
                break;
            case _EStateType_.estDie:
                myState = gameObject.AddComponent<PlayerDieState>();
                myState.DoAction(_type);
                break;
        }
    }
}
