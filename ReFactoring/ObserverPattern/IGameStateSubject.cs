using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStateSubject
{
    void NotifyGameState(_EGameStateType_ _type, bool _state);
    void AddObserver(IGameStateObserver _ob);
    void RemoveObserver(IGameStateObserver _ob);
}
