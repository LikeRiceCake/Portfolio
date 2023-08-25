using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashState : PlayerState
{
    const float DASH_RANGEMULTI = 3.5f;
    const float DASHTIME = 0.5f;

    public override void DoAction(_EStateType_ state)
    {
        myAnim.SetTrigger("Dash");
        gameObject.AddComponent<DashCoolDown>();
        DashFunc();
    }

    void DashFunc()
    {
        GetComponent<CharacterController>().Move(transform.forward * DASH_RANGEMULTI);
        SwitchLayer(LayerMask.NameToLayer("PlayerDash"));
        Invoke("PlayerDashOut", DASHTIME);
    }
    
    void PlayerDashOut()
    {
        SwitchLayer(LayerMask.NameToLayer("Player"));
        stateManager.SetActionType(_EStateType_.eIdle, _EObjectType_.ePlayer);
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
