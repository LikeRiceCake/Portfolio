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
            case _EMonsterType_.eBoar:
            case _EMonsterType_.eBird:
            case _EMonsterType_.eWolf:
                stateManager = gameObject.AddComponent<NormalStateManager>();
                break;
            case _EMonsterType_.eGumihoH:
            case _EMonsterType_.eGumihoA:
            case _EMonsterType_.eHeogho:
                stateManager = gameObject.AddComponent<BossStateManager>();
                break;
            default:
                stateManager = gameObject.AddComponent<StateManager>();
                break;
        }
    }

    public override void ControlAction()
    {
        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.eMonster);
    }
}
