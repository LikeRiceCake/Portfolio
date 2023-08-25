using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStateObserver
{
    void ReactNotify(_EGameStateType_ type, bool state);
}
