using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField]
    CutSceneUIChannelSO myChannel;

    [SerializeField]
    GameObject[] uis;

    private void Start()
    {
        myChannel.uis = uis;
    }
}
