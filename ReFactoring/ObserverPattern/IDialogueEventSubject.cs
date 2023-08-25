using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueEventSubject
{
    void Notify(_EDialogueEventType_ type);
    void AddObserver(IDialogueEventObserver ob);
    void RemoveObserver(IDialogueEventObserver ob);
}
