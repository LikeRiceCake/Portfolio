using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneUI : MonoBehaviour
{
    [SerializeField]
    CutSceneUIChannelSO myChannel;

    public void OnUIs()
    {
        foreach (var ui in myChannel.uis)
            ui.SetActive(true);
    }
    
    public void OffUIs()
    {
        foreach (var ui in myChannel.uis)
            ui.SetActive(false);
    }
}
