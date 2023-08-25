using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour, IDieSubject
{
    protected List<IDieObserver> myDieObs;

    public _EStateType_ currentState { get; set; }

    protected State myState;

    public abstract void SetActionType(_EStateType_ newState, _EObjectType_ type);

    public abstract void NotifyDeath();

    public abstract void AddObserver(IDieObserver ob);

    public abstract void RemoveObserver(IDieObserver ob);

    private void Awake()
    {
        myDieObs = new List<IDieObserver>();
    }
}

