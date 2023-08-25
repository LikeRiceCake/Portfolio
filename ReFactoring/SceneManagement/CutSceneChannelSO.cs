using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CutSceneChannelSO", menuName = "Scriptable/CutSceneChannelSO")]
public class CutSceneChannelSO : ScriptableObject
{
    public UnityAction<_ECutSceneType_> myEvent;

    public void RaiseEvent(_ECutSceneType_ type)
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
