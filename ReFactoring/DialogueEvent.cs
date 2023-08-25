using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour, IDialogueEventObserver
{
    [SerializeField]
    InitHelperChannelSO myChannel;

    Dialogue dialogue;

    MonsterSpawner monsterSpawner;

    ObjectOpener objectOpener;

    PlayerSkillManager playerSkillManager;

    MapLoader mapLoader;

    InitHelper initHelper;

    const float MOVE_TIME_LIMIT = 5f;
    const int ATTACK_COUNT_LIMIT = 7;
    const int SKILL_COUNT_LIMIT = 4;
    const int DASH_COUNT_LIMIT = 2;

    const float DIALOGUE_START_TIME = 1.5f;

    const float GET_SKILL_TIME = 3f;

    const int BEFORE_SECONDROOM_DIECNT = 6;
    const int SECOND_THIRDROOM_DIECNT = 4;
    const int THIRDROOM_DIECNT = 6;
    const int THIRD_MIDDLEDBOSSROOM_DIECNT = 3;
    const int MIDDLEDBOSSROOM_DIECNT = 1;
    const int FINALBOSSROOM_DIECNT = 1;

    int huntCnt;

    public void AddMyFunc()
    {
        GameObject.FindWithTag("Tutorial").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("BeforeSecondRoom").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("FrontNotBrokenWall").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("BeforeMiddleBossRoom").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("EnterMiddleBossRoom").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("FrontAnotherWall").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("PondCrystal").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("BeforeFinalBossRoom").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("SkillWall(CanBrake)").GetComponent<IDialogueEventSubject>().AddObserver(this);
        GameObject.FindWithTag("SkillWall(CantBrake)").GetComponent<IDialogueEventSubject>().AddObserver(this);
        FindObjectOfType<DieManager>().GetComponent<IDialogueEventSubject>().AddObserver(this);
    }

    public void ReactNotify(_EDialogueEventType_ type)
    {
        switch (type)
        {
            case _EDialogueEventType_.eTutorial:
            case _EDialogueEventType_.eBeforeSecondRoom:
            case _EDialogueEventType_.eEndThirdRoomBattle:
            case _EDialogueEventType_.eFrontNotBrokenWall:
            case _EDialogueEventType_.eBeforeMiddleBossRoom:
            case _EDialogueEventType_.eEnterMiddleBossRoom:
            case _EDialogueEventType_.eFrontCanBrokenWall:
            case _EDialogueEventType_.eEndBrakeWall:
            case _EDialogueEventType_.eFrontAnotherWall:
            case _EDialogueEventType_.eEndShotWall:
            case _EDialogueEventType_.ePondCrystal:
            case _EDialogueEventType_.eBeforeFinalBossRoom:
                dialogue.OnLine(type);
                break;
            case _EDialogueEventType_.eMonsterDie:
                huntCnt++;
                break;
        }
    }

    private void Awake()
    {
        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();
    }

    void Start()
    {
        playerSkillManager = GameObject.Find("Player").GetComponent<PlayerSkillManager>();

        objectOpener = GameObject.Find("BlockWall").GetComponent<ObjectOpener>();

        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        dialogue = GetComponent<Dialogue>();

        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();

        Invoke("StartDialogue", DIALOGUE_START_TIME);

        AddMyFunc();

        initHelper.needInit[(int)_EInitCallType_.eEnter_MiddleBossRoom].Add(Init_Enter_MiddleBossRoom);
    }

    void StartDialogue()
    {
        dialogue.OnLine(_EDialogueEventType_.eStart);
    }

    public void DoEvent(_EDialogueEventType_ type)
    {
        StartCoroutine(ActivateEvent(type));
    }

    IEnumerator ActivateEvent(_EDialogueEventType_ type)
    {
        switch (type)
        {
            case _EDialogueEventType_.eTutorial:
                float moveTime = 0f;

                while (true)
                {
                    if (moveTime >= MOVE_TIME_LIMIT)
                        break;

                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                        moveTime += Time.deltaTime;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eTutorialAttack);
                break;
            case _EDialogueEventType_.eTutorialAttack:
                int attackCnt = 0;

                while (true)
                {
                    if (attackCnt >= ATTACK_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
                        attackCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eTutorialSkill);
                break;
            case _EDialogueEventType_.eTutorialSkill:
                int skillCnt = 0;

                while (true)
                {
                    if (skillCnt >= SKILL_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                        skillCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eTutorialDash);
                break;
            case _EDialogueEventType_.eTutorialDash:
                int dashCnt = 0;

                while (true)
                {
                    if (dashCnt >= DASH_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                        dashCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eTutorialEnd);
                break;
            case _EDialogueEventType_.eBeforeSecondRoom:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eSecondRoom);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eSecondRoom);
                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= BEFORE_SECONDROOM_DIECNT)
                        break;  

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eEndSecondRoomBattle);
                break;
            case _EDialogueEventType_.eEndSecondRoomBattle:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eSecondToThirdWay);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eSecondToThirdWay);

                objectOpener.OpenWall(_EOpenableWall_.eSecondRoomToThirdRoomWall);
                
                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= SECOND_THIRDROOM_DIECNT)
                        break;

                    yield return null;
                }
                DoEvent(_EDialogueEventType_.eEndSecondThirdRoomBattle);
                break;
            case _EDialogueEventType_.eEndSecondThirdRoomBattle:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eThirdRoom);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eThirdRoom);

                objectOpener.OpenWall(_EOpenableWall_.eThirdRoom);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= THIRDROOM_DIECNT)
                        break;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.eEndThirdRoomBattle);
                break;
            case _EDialogueEventType_.eEndThirdRoomBattle:
                objectOpener.OpenWall(_EOpenableWall_.eThirdRoomToFourthRoom);
                break;
            case _EDialogueEventType_.eFrontNotBrokenWall:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eThirdToMiddleBoss);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eThirdToMiddleBoss);

                objectOpener.OpenWall(_EOpenableWall_.eThirdRoomToMiddleBossRoom);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= THIRD_MIDDLEDBOSSROOM_DIECNT)
                        break;

                    yield return null;
                }
                objectOpener.OpenWall(_EOpenableWall_.eMiddleBossRoom);
                break;
            case _EDialogueEventType_.eEnterMiddleBossRoom:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eMiddleBossRoom_H);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eMiddleBossRoom_H);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= MIDDLEDBOSSROOM_DIECNT)
                        break;

                    yield return null;
                }
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eMiddleBossRoom_Idle);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.eMiddleBossRoom_Idle);

                dialogue.OnLine(_EDialogueEventType_.eEndMiddleBossRoomBattle);
                break;
            case _EDialogueEventType_.eEndMiddleBossRoomBattle:
                float getSkillTime = 0f;

                while (true)
                {
                    if (getSkillTime >= GET_SKILL_TIME)
                        break;

                    getSkillTime += Time.deltaTime;

                    yield return null;
                }
                playerSkillManager.PlayerGetSkill(_ESkillType_.eGumiho);
                objectOpener.OepnTrigger(_EOpenableTrigger_.eFrontCanBrokenWall);
                dialogue.OnLine(_EDialogueEventType_.eGetGumihoSkill);
                break;
            case _EDialogueEventType_.ePondCrystal:
                objectOpener.OpenWall(_EOpenableWall_.eFinalBossRoom);
                break;
            case _EDialogueEventType_.eBeforeFinalBossRoom:
                mapLoader.StartLoadMap(_EMapType_.eFinalBossAppear);
                monsterSpawner.CreateMonsters(_ESpawnStageType_.eFinalBossRoom);
                break;
            case _EDialogueEventType_.eEndInstantiateFinalBoss:
                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= FINALBOSSROOM_DIECNT)
                        break;

                    yield return null;
                }
                mapLoader.StartLoadMap(_EMapType_.eFinalBossRepel);
                break;
        }
    }

    public void Init_Enter_MiddleBossRoom()
    {
        GameObject.Find("FrontCanBrokenWall").GetComponent<IDialogueEventSubject>().AddObserver(this);
    }
}
