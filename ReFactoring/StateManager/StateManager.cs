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

    public abstract void SetActionType(_EStateType_ _type, _EObjectType_ _dtype);

    public abstract void NotifyDeath();

    public abstract void AddObserver(IDieObserver _ob);

    public abstract void RemoveObserver(IDieObserver _ob);

    private void Awake()
    {
        myDieObs = new List<IDieObserver>();
    }
}

