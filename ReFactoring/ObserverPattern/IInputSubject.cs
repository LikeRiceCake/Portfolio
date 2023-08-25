using PublicEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSubject
{
    void AddObserver(IInputKeyClickObserver ob);
    void RemoveObserver(IInputKeyClickObserver ob);
}