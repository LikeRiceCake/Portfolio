#ifndef _ENUMS_H_
#define _ENUMS_H_

enum _EJob_ {
	eJobNone = 0,
	eWarrior,
	eArcher,
	eAssassin,
	eGuardian,
	eGunner,
	eJobMax
};

enum _ERace_ {
	eRaceNone = 0,
	eHuman,
	eOrc,
	eElf,
	eDwarf,
	eRaceMax
};

enum _EStat_ {
	eStatNone = 0,
	eHp,
	eSp,
	eMp,
	eMoney,
	eXY
};

enum _EMonsters_ {
	eMonsterTypeNone = 0,
	eZombie,
	eSpider,
	eSkeleton,
	eNaga,
	eBooooss
};

enum _EFight_ {
	eAttack = 1,
	eRaceSkill,
	eJobSkill,
	eUniqueSkill
};

#endif