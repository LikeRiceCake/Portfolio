using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputKeyClickObserver
{
    void ReactNotify(_EInputType_ _type, _EInputDetailType_ _dType);
}