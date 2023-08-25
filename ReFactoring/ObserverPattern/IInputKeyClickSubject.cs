using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputKeyClickSubject
{
    void NotifyKeyClickInput(_EInputType_ type, _EInputDetailType_ dType);
    void AddObserver(IInputKeyClickObserver ob);
    void RemoveObserver(IInputKeyClickObserver ob);
}
