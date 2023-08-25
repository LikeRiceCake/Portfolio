using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PublicEnums
{
    public enum _EDialogueEventType_
    {
        eStart,
        eTutorial,
        eTutorialMove,
        eTutorialAttack,
        eTutorialSkill,
        eTutorialDash,
        eTutorialEnd,
        eBeforeSecondRoom,
        eEndSecondRoomBattle,
        eEndSecondThirdRoomBattle,
        eEndThirdRoomBattle,
        eFrontNotBrokenWall,
        eBeforeMiddleBossRoom,
        eEnterMiddleBossRoom,
        eEndMiddleBossRoomBattle,
        eGetGumihoSkill,
        eFrontCanBrokenWall,
        eEndBrakeWall,
        eFrontAnotherWall,
        eEndShotWall,
        ePondCrystal,
        eBeforeFinalBossRoom,
        eEndInstantiateFinalBoss,
        eMonsterDie,
        eMax
    }

    public enum _EInputType_
    {
        eMove,
        eRotate,
        eSkill,
        eDash,
        eOption,
        eDialogue,
        eAttack,
        eMax
    }

    public enum _EInputDetailType_
    {
        eDown_W,
        eDown_S,
        eDown_A,
        eDown_D,
        eUp_W,
        eUp_S,
        eUp_A,
        eUp_D,
        eRotate,
        eChangeSkill,
        eUseSkill,
        eDash,
        eOption,
        eDialogue,
        eAttack_W,
        eAttack_S,
        eMax
    }

    public enum _EMapType_
    {
        eTitle,
        eIntro,
        eInGame,
        eMiddleBossTransformation,
        eFinalBossAppear,
        eFinalBossRepel,
        eGumihoFight,
        eHeoghoFight,
        eMax
    }

    public enum _ECutSceneType_
    {
        
        eMax
    }

    public enum _ESoundType_
    {
        eMaster,
        eBGM,
        eSFX,
        eMax
    }

    public enum _EPlayerDamageType_
    {
        eWeak_1,
        eWeak_2,
        eWeak_3,
        eW1_Strong_1,
        eW2_Strong_2,
        eW3_Strong_3,
        eMax
    }

    namespace Monster
    {
        public enum _EMonsterType_
        {
            eBoar,
            eBird,
            eWolf,
            eGumihoH,
            eGumihoA,
            eGumihoIdle,
            eHeogho,
            eMax
        }

        namespace DetailType
        {
            public enum _ENormalType_
            {
                eBoar,
                eBird,
                eWolf,
                eMax
            }

            public enum _EBossType_
            {
                eGumihoH,
                eGumihoA,
                eGumihoIdle,
                eHeogho,
                eMax
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
                ePlayer,
                eBoss,
                eMax
            }
        }
    }

    namespace State
    {
        public enum _EStateType_
        {
            eIdle,
            eMove,
            eDash,
            eAttack,
            eChase,
            eBattleIdle,
            eSkill,
            eLine,
            eOption,
            eDamaged,
            eStagger,
            eGroggy,
            eChangePattern,
            eDie,
            eMax
        }

        public enum _EGameStateType_
        {
            eisStop,
            eisLine,
            eisOption,
            eMax
        }

        public enum _ESkillType_
        {
            eCrow,
            eGumiho,
            eHeogho,
            eMax
        }
    }

    public enum _EObjectType_
    {
        ePlayer,
        eMonster,
        eNormal,
        eBoss,
        eMax
    }

    public enum _EIntStatType_
    {
        eMaxHp,
        eCurrentHp,
        eDamage,
        eDamage_Sec,
        eDamage_Thi,
        eDamage_For,
        eDamage_Fif,
        eDamage_Six,
        eMax
    }

    public enum _EFloatStatType_
    {
        eSpeed,
        eAttackRange,
        eSight,
        eAttackCool,
        eCurrentAttackCool,
        eMax
    }

    public enum _EPlayerAttackType_
    {
        eWeak,
        eStrong,
        eMax
    }
    
    public enum _ESpawnStageType_
    {
        eSecondRoom,
        eSecondToThirdWay,
        eThirdRoom,
        eThirdToMiddleBoss,
        eMiddleBossRoom_H,
        eMiddleBossRoom_A,
        eMiddleBossRoom_Idle,
        eFinalBossRoom,
        eMax
    }

    public enum _EInitCallType_
    {
        eEnter_InGameScene,
        eEnter_MiddleBossRoom,
        eMax
    }

    public enum _EOpenableWall_
    {
        eSecondRoomToThirdRoomWall,
        eThirdRoom,
        eThirdRoomToFourthRoom,
        eThirdRoomToMiddleBossRoom,
        eMiddleBossRoom,
        eFourthRoom,
        eFinalBossRoom,
        eMax
    }

    public enum _EOpenableTrigger_
    {
        eFrontCanBrokenWall,
        eMax
    }
}