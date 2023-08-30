using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputStrategy : ControlStrategy
{
    StateManager stateManager;

    void Awake()
    {
        stateManager = gameObject.AddComponent<PlayerStateManager>();

        gameObject.AddComponent<PlayerSkillManager>();
    }

    public override void ControlAction()
    {
        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
    }
}
