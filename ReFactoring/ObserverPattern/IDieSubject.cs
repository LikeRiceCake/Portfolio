using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDieSubject
{
    void NotifyDeath();
    void AddObserver(IDieObserver ob);
    void RemoveObserver(IDieObserver ob);
}
