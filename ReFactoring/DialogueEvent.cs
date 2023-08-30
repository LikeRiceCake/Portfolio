using PublicEnums;
using PublicEnums.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour, IDialogueEventObserver
{
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

    public void ReactNotify(_EDialogueEventType_ _type)
    {
        switch (_type)
        {
            case _EDialogueEventType_.edetTutorial:
            case _EDialogueEventType_.edetBeforeSecondRoom:
            case _EDialogueEventType_.edetEndThirdRoomBattle:
            case _EDialogueEventType_.edetFrontNotBrokenWall:
            case _EDialogueEventType_.edetBeforeMiddleBossRoom:
            case _EDialogueEventType_.edetEnterMiddleBossRoom:
            case _EDialogueEventType_.edetFrontCanBrokenWall:
            case _EDialogueEventType_.edetEndBrakeWall:
            case _EDialogueEventType_.edetFrontAnotherWall:
            case _EDialogueEventType_.edetEndShotWall:
            case _EDialogueEventType_.edetPondCrystal:
            case _EDialogueEventType_.edetBeforeFinalBossRoom:
                dialogue.OnLine(_type);
                break;
            case _EDialogueEventType_.edetMonsterDie:
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

        initHelper.needInit[(int)_EInitCallType_.eictEnter_MiddleBossRoom].Add(Init_Enter_MiddleBossRoom);
    }

    void StartDialogue()
    {
        dialogue.OnLine(_EDialogueEventType_.eStart);
    }

    public void DoEvent(_EDialogueEventType_ _type)
    {
        StartCoroutine(ActivateEvent(_type));
    }

    IEnumerator ActivateEvent(_EDialogueEventType_ _type)
    {
        switch (_type)
        {
            case _EDialogueEventType_.edetTutorial:
                float moveTime = 0f;

                while (true)
                {
                    if (moveTime >= MOVE_TIME_LIMIT)
                        break;

                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                        moveTime += Time.deltaTime;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetTutorialAttack);
                break;
            case _EDialogueEventType_.edetTutorialAttack:
                int attackCnt = 0;

                while (true)
                {
                    if (attackCnt >= ATTACK_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
                        attackCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetTutorialSkill);
                break;
            case _EDialogueEventType_.edetTutorialSkill:
                int skillCnt = 0;

                while (true)
                {
                    if (skillCnt >= SKILL_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                        skillCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetTutorialDash);
                break;
            case _EDialogueEventType_.edetTutorialDash:
                int dashCnt = 0;

                while (true)
                {
                    if (dashCnt >= DASH_COUNT_LIMIT)
                        break;

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                        dashCnt++;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetTutorialEnd);
                break;
            case _EDialogueEventType_.edetBeforeSecondRoom:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstSecondRoom);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstSecondRoom);
                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= BEFORE_SECONDROOM_DIECNT)
                        break;  

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetEndSecondRoomBattle);
                break;
            case _EDialogueEventType_.edetEndSecondRoomBattle:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstSecondToThirdWay);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstSecondToThirdWay);

                objectOpener.OpenWall(_EOpenableWall_.eowSecondRoomToThirdRoomWall);
                
                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= SECOND_THIRDROOM_DIECNT)
                        break;

                    yield return null;
                }
                DoEvent(_EDialogueEventType_.edetEndSecondThirdRoomBattle);
                break;
            case _EDialogueEventType_.edetEndSecondThirdRoomBattle:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstThirdRoom);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstThirdRoom);

                objectOpener.OpenWall(_EOpenableWall_.eowThirdRoom);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= THIRDROOM_DIECNT)
                        break;

                    yield return null;
                }
                dialogue.OnLine(_EDialogueEventType_.edetEndThirdRoomBattle);
                break;
            case _EDialogueEventType_.edetEndThirdRoomBattle:
                objectOpener.OpenWall(_EOpenableWall_.eowThirdRoomToFourthRoom);
                break;
            case _EDialogueEventType_.edetFrontNotBrokenWall:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstThirdToMiddleBoss);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstThirdToMiddleBoss);

                objectOpener.OpenWall(_EOpenableWall_.eowThirdRoomToMiddleBossRoom);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= THIRD_MIDDLEDBOSSROOM_DIECNT)
                        break;

                    yield return null;
                }
                objectOpener.OpenWall(_EOpenableWall_.eowMiddleBossRoom);
                break;
            case _EDialogueEventType_.edetEnterMiddleBossRoom:
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstMiddleBossRoom_H);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstMiddleBossRoom_H);

                huntCnt = 0;

                while (true)
                {
                    if (huntCnt >= MIDDLEDBOSSROOM_DIECNT)
                        break;

                    yield return null;
                }
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstMiddleBossRoom_Idle);
                monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstMiddleBossRoom_Idle);

                dialogue.OnLine(_EDialogueEventType_.edetEndMiddleBossRoomBattle);
                break;
            case _EDialogueEventType_.edetEndMiddleBossRoomBattle:
                float getSkillTime = 0f;

                while (true)
                {
                    if (getSkillTime >= GET_SKILL_TIME)
                        break;

                    getSkillTime += Time.deltaTime;

                    yield return null;
                }
                playerSkillManager.AddSkill(_ESkillType_.estGumiho);
                objectOpener.OepnTrigger(_EOpenableTrigger_.eotFrontCanBrokenWall);
                dialogue.OnLine(_EDialogueEventType_.edetGetGumihoSkill);
                break;
            case _EDialogueEventType_.edetPondCrystal:
                objectOpener.OpenWall(_EOpenableWall_.eowFinalBossRoom);
                break;
            case _EDialogueEventType_.edetBeforeFinalBossRoom:
                mapLoader.StartLoadMap(_EMapType_.eFinalBossAppear);
                monsterSpawner.CreateMonsters(_ESpawnStageType_.esstFinalBossRoom);
                break;
            case _EDialogueEventType_.edetEndInstantiateFinalBoss:
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
