using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueEventObserver
{
    void ReactNotify(_EDialogueEventType_ _type);

    void AddMyFunc();
}
