using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOpener : MonoBehaviour
{
    GameObject[] walls;
    GameObject[] triggers;

    InitHelper initHelper;

    private void Awake()
    {
        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();
    }

    public void Start()
    {
        walls = new GameObject[(int)_EOpenableWall_.eowMax];

        walls[(int)_EOpenableWall_.eowSecondRoomToThirdRoomWall] = GameObject.Find("BlockWall").transform.Find("SecondRoomToThirdRoom").gameObject;
        walls[(int)_EOpenableWall_.eowThirdRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoom").gameObject;
        walls[(int)_EOpenableWall_.eowThirdRoomToFourthRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoomToFourthRoom").gameObject;
        walls[(int)_EOpenableWall_.eowThirdRoomToMiddleBossRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoomToMiddleBossRoom").gameObject;
        walls[(int)_EOpenableWall_.eowMiddleBossRoom] = GameObject.Find("BlockWall").transform.Find("MiddleBossRoom").gameObject;
        walls[(int)_EOpenableWall_.eowFourthRoom] = GameObject.Find("BlockWall").transform.Find("FourthRoom").gameObject;
        walls[(int)_EOpenableWall_.eowFinalBossRoom] = GameObject.Find("BlockWall").transform.Find("FinalBossRoom").gameObject;

        triggers = new GameObject[(int)_EOpenableTrigger_.eotMax];

        triggers[(int)_EOpenableTrigger_.eotFrontCanBrokenWall] = GameObject.Find("DialogueTriggers").transform.Find("FrontCanBrokenWall").gameObject;
    }

    public void CloseWall(_EOpenableWall_ _type)
    {
        walls[(int)_type].SetActive(true);
    }
    
    public void OpenWall(_EOpenableWall_ _type)
    {
        walls[(int)_type].SetActive(false);
    }

    public void OepnTrigger(_EOpenableTrigger_ _type)
    {
        triggers[(int)_type].SetActive(true);
        initHelper.CallInit(_EInitCallType_.eictEnter_MiddleBossRoom);
    }
}
