using PublicEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitHelper : MonoBehaviour
{
    public List<Action>[] needInit { get; private set; }

    private void Awake()
    {
        needInit = new List<Action>[(int)_EInitCallType_.eMax];

        for (int i = 0; i < needInit.Length; i++)
            needInit[i] = new List<Action>();
    }

    public void CallInit(_EInitCallType_ type)
    {
        foreach(var action in needInit[(int)type])
            action.Invoke();
    }
}
