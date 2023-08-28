using PublicEnums;
using PublicEnums.Monster;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : ControlStrategy
{
    StateManager stateManager;

    private void Awake()
    {
        switch (GetComponent<Monster>().GetMonsterType())
        {
            case _EMonsterType_.emtBoar:
            case _EMonsterType_.emtBird:
            case _EMonsterType_.emtWolf:
                stateManager = gameObject.AddComponent<NormalStateManager>();
                break;
            case _EMonsterType_.emtGumihoH:
            case _EMonsterType_.emtGumihoA:
            case _EMonsterType_.emtHeogho:
                stateManager = gameObject.AddComponent<BossStateManager>();
                break;
            default:
                stateManager = gameObject.AddComponent<StateManager>();
                break;
        }
    }

    public override void ControlAction()
    {
        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotMonster);
    }
}
