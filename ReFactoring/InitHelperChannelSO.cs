using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InitHelperChannelSO", menuName = "Scriptable/InitHelperChannelSO")]
public class InitHelperChannelSO : ScriptableObject
{
    public List<IInit> needInit { get; set; }
}
