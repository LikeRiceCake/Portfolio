#ifndef _PLAYER_H_
#define _PLAYER_H_

#include "Object.h"
#include "Enums.h"

class Player : virtual public Object
{
public:
	Player();
	~Player();

	void Move();
	void Attack(Object* Monsters);
	void Rest();
	virtual void Fight(int nValue, Object* Monsters);
	int GetStat(_EStat_ eStat);
	virtual void ReadInfo();
	void InitRaceInfo(int nRaceValue);
	void InitJobInfo(int nJobValue);
	int GetRaceSkillCount();
	bool GetIsRaceSkill();
	void SetRaceSkillCount(int nValue);
	void SetIsRaceSkill(bool bValue);

protected:
	int m_nMaxSp;
	int m_nMySp;
	int m_nMaxMp;
	int m_nMyMp;
	int m_nJobSkillDamage;
	int m_nRaceSkillCount;
	bool m_bIsRaceSkill;
	char* m_nMyRace;
	char* m_nMyJob;
};

#endif
