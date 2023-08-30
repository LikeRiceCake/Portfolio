using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashState : PlayerState
{
    const float DASH_RANGE_MULTI = 3.5f;
    const float DASH_TIME = 0.5f;

    public override void DoAction(_EStateType_ _type)
    {
        myAnim.SetTrigger("Dash");
        gameObject.AddComponent<DashCoolDown>();
        DashFunc();
    }

    void DashFunc()
    {
        GetComponent<CharacterController>().Move(transform.forward * DASH_RANGE_MULTI);
        SwitchLayer(LayerMask.NameToLayer("PlayerDash"));
        Invoke("PlayerDashOut", DASH_TIME);
    }
    
    void PlayerDashOut()
    {
        SwitchLayer(LayerMask.NameToLayer("Player"));
        stateManager.SetActionType(_EStateType_.estIdle, _EObjectType_.eotPlayer);
    }

    void SwitchLayer(LayerMask _layer)
    {
        gameObject.layer = _layer;
    }

    public override void ExitState()
    {
        if(gameObject.layer != LayerMask.NameToLayer("Player"))
            SwitchLayer(LayerMask.NameToLayer("Player"));
    }
}
