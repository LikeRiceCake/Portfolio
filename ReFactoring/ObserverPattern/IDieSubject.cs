using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDieSubject
{
    void NotifyDeath();
    void AddObserver(IDieObserver _ob);
    void RemoveObserver(IDieObserver _ob);
}
