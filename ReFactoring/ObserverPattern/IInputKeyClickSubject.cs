using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputKeyClickSubject
{
    void NotifyKeyClickInput(_EInputType_ _type, _EInputDetailType_ _dType);
    void AddObserver(IInputKeyClickObserver _ob);
    void RemoveObserver(IInputKeyClickObserver _ob);
}
