using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadMapChannelSO", menuName = "Scriptable/LoadMapChannelSO")]
public class LoadMapChannelSO : ScriptableObject
{
    public UnityAction<_EMapType_> myEvent;

    public void RaiseEvent(_EMapType_ type)
    {
        if (myEvent != null)
        {
            myEvent.Invoke(type);
        }
        else
        {
            Debug.LogWarning("이벤트가 비어있습니다.");
        }
    }
}
