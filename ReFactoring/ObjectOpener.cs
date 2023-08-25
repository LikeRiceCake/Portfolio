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
        walls = new GameObject[(int)_EOpenableWall_.eMax];

        walls[(int)_EOpenableWall_.eSecondRoomToThirdRoomWall] = GameObject.Find("BlockWall").transform.Find("SecondRoomToThirdRoom").gameObject;
        walls[(int)_EOpenableWall_.eThirdRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoom").gameObject;
        walls[(int)_EOpenableWall_.eThirdRoomToFourthRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoomToFourthRoom").gameObject;
        walls[(int)_EOpenableWall_.eThirdRoomToMiddleBossRoom] = GameObject.Find("BlockWall").transform.Find("ThirdRoomToMiddleBossRoom").gameObject;
        walls[(int)_EOpenableWall_.eMiddleBossRoom] = GameObject.Find("BlockWall").transform.Find("MiddleBossRoom").gameObject;
        walls[(int)_EOpenableWall_.eFourthRoom] = GameObject.Find("BlockWall").transform.Find("FourthRoom").gameObject;
        walls[(int)_EOpenableWall_.eFinalBossRoom] = GameObject.Find("BlockWall").transform.Find("FinalBossRoom").gameObject;

        triggers = new GameObject[(int)_EOpenableTrigger_.eMax];

        triggers[(int)_EOpenableTrigger_.eFrontCanBrokenWall] = GameObject.Find("DialogueTriggers").transform.Find("FrontCanBrokenWall").gameObject;
    }

    public void CloseWall(_EOpenableWall_ wall)
    {
        walls[(int)wall].SetActive(true);
    }
    
    public void OpenWall(_EOpenableWall_ wall)
    {
        walls[(int)wall].SetActive(false);
    }

    public void OepnTrigger(_EOpenableTrigger_ trigger)
    {
        triggers[(int)trigger].SetActive(true);
        initHelper.CallInit(_EInitCallType_.eEnter_MiddleBossRoom);
    }
}
