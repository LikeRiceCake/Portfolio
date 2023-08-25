using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputKeyClickObserver
{
    void ReactNotify(_EInputType_ type, _EInputDetailType_ dType);
}