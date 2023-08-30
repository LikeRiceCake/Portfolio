using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueEventSubject
{
    void Notify(_EDialogueEventType_ _type);
    void AddObserver(IDialogueEventObserver _ob);
    void RemoveObserver(IDialogueEventObserver _ob);
}
