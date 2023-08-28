using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PublicEnums
{
    public enum _EDialogueEventType_
    {
        eStart,
        edetTutorial,
        edetTutorialMove,
        edetTutorialAttack,
        edetTutorialSkill,
        edetTutorialDash,
        edetTutorialEnd,
        edetBeforeSecondRoom,
        edetEndSecondRoomBattle,
        edetEndSecondThirdRoomBattle,
        edetEndThirdRoomBattle,
        edetFrontNotBrokenWall,
        edetBeforeMiddleBossRoom,
        edetEnterMiddleBossRoom,
        edetEndMiddleBossRoomBattle,
        edetGetGumihoSkill,
        edetFrontCanBrokenWall,
        edetEndBrakeWall,
        edetFrontAnotherWall,
        edetEndShotWall,
        edetPondCrystal,
        edetBeforeFinalBossRoom,
        edetEndInstantiateFinalBoss,
        edetMonsterDie,
        edetMax
    }

    public enum _EInputType_
    {
        eitMove,
        eitRotate,
        eitSkill,
        eitDash,
        eitOption,
        eitDialogue,
        eitAttack,
        eitMax
    }

    public enum _EInputDetailType_
    {
        eidtDown_W,
        eidtDown_S,
        eidtDown_A,
        eidtDown_D,
        eidtUp_W,
        eidtUp_S,
        eidtUp_A,
        eidtUp_D,
        eidtRotate,
        eidtChangeSkill,
        eidtUseSkill,
        eidtDash,
        eidtOption,
        eidtDialogue,
        eidtAttack_W,
        eidtAttack_S,
        eidtMax
    }

    public enum _EMapType_
    {
        emtTitle,
        emtIntro,
        emtInGame,
        emtMiddleBossTransformation,
        emtFinalBossAppear,
        emtFinalBossRepel,
        emtGumihoFight,
        emtHeoghoFight,
        emtMax
    }

    public enum _ESoundType_
    {
        estMaster,
        estBGM,
        estSFX,
        estMax
    }

    public enum _EPlayerDamageType_
    {
        epdtWeak_1,
        epdtWeak_2,
        epdtWeak_3,
        epdtW1_Strong_1,
        epdtW2_Strong_2,
        epdtW3_Strong_3,
        epdtMax
    }

    namespace Monster
    {
        public enum _EMonsterType_
        {
            emtBoar,
            emtBird,
            emtWolf,
            emtGumihoH,
            emtGumihoA,
            emtGumihoIdle,
            emtHeogho,
            emtMax
        }

        namespace DetailType
        {
            public enum _ENormalType_
            {
                entBoar,
                entBird,
                entWolf,
                entMax
            }

            public enum _EBossType_
            {
                ebtGumihoH,
                ebtGumihoA,
                ebtGumihoIdle,
                ebtHeogho,
                ebtMax
            }
        }
    }


    public enum _ECharacterImageType_
    {
        God,
        Zero,
        Tam,
        Gumiho,
        Heogho,
        eMax
    }


    namespace UI
    {
        namespace HP
        {
            public enum _EHPUIType_
            {
                ehutPlayer,
                ehutBoss,
                ehutMax
            }
        }
    }

    namespace State
    {
        public enum _EStateType_
        {
            estIdle,
            estMove,
            estDash,
            estAttack,
            estChase,
            estBattleIdle,
            estSkill,
            estLine,
            estOption,
            estDamaged,
            estStagger,
            estGroggy,
            estChangePattern,
            estDie,
            estMax
        }

        public enum _EGameStateType_
        {
            egstIsStop,
            egstIsLine,
            egstIsOption,
            egstMax
        }

        public enum _ESkillType_
        {
            estCrow,
            estGumiho,
            estHeogho,
            estMax
        }
    }

    public enum _EObjectType_
    {
        eotPlayer,
        eotMonster,
        eotNormal,
        eotBoss,
        eotMax
    }

    public enum _EIntStatType_
    {
        eistMaxHp,
        eistCurrentHp,
        eistDamage,
        eistDamage_Sec,
        eistDamage_Thi,
        eistDamage_For,
        eistDamage_Fif,
        eistDamage_Six,
        eistMax
    }

    public enum _EFloatStatType_
    {
        efstSpeed,
        efstAttackRange,
        efstSight,
        efstAttackCool,
        efstCurrentAttackCool,
        efstMax
    }

    public enum _EPlayerAttackType_
    {
        epatWeak,
        epatStrong,
        epatMax
    }

    public enum _ESpawnStageType_
    {
        esstSecondRoom,
        esstSecondToThirdWay,
        esstThirdRoom,
        esstThirdToMiddleBoss,
        esstMiddleBossRoom_H,
        esstMiddleBossRoom_A,
        esstMiddleBossRoom_Idle,
        esstFinalBossRoom,
        esstMax
    }

    public enum _EInitCallType_
    {
        eictEnter_InGameScene,
        eictEnter_MiddleBossRoom,
        eictMax
    }

    public enum _EOpenableWall_
    {
        eowSecondRoomToThirdRoomWall,
        eowThirdRoom,
        eowThirdRoomToFourthRoom,
        eowThirdRoomToMiddleBossRoom,
        eowMiddleBossRoom,
        eowFourthRoom,
        eowFinalBossRoom,
        eowMax
    }

    public enum _EOpenableTrigger_
    {
        eotFrontCanBrokenWall,
        eotMax
    }
}