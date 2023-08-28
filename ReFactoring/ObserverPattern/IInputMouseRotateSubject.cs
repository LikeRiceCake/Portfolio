using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputMouseRotateSubject
{
    void NotifyMouseRotate(_EInputType_ _type, _EInputDetailType_ _dType, Vector2 _value);
    void AddObserver(IInputMouseRotateObserver _ob);
    void RemoveObserver(IInputMouseRotateObserver _ob);
}
