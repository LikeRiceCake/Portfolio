using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDieObserver
{
    void ReactNotify(_EObjectType_ type);

    void AddMyFunc(IDieSubject die);
}
