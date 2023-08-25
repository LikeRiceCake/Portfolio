using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStateSubject
{
    void NotifyGameState(_EGameStateType_ type, bool state);
    void AddObserver(IGameStateObserver ob);
    void RemoveObserver(IGameStateObserver ob);
}
