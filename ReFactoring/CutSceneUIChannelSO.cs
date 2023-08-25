using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutSceneUIChannelSO", menuName = "Scriptable/CutSceneUIChannelSO")]
public class CutSceneUIChannelSO : ScriptableObject
{
    public GameObject[] uis { get; set; }
}
