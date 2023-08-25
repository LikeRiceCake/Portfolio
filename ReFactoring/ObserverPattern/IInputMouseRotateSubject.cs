using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputMouseRotateSubject
{
    void NotifyMouseRotate(_EInputType_ type, _EInputDetailType_ dType, Vector2 value);
    void AddObserver(IInputMouseRotateObserver ob);
    void RemoveObserver(IInputMouseRotateObserver ob);
}
