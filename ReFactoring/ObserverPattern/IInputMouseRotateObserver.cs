using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputMouseRotateObserver
{
    void ReactNotify(_EInputType_ type, _EInputDetailType_ dType, Vector2 value);
}