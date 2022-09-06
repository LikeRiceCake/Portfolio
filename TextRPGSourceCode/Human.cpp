#include "Human.h"
#include "Tools.h"

Human::Human()
{
	InitRaceInfo(eHuman);
}

void Human::UseGrit(Object* Monsters)
{
	if (GetRaceSkillCount() > 10 && !GetIsRaceSkill())
	{
		SetIsRaceSkill(true);
		SetRaceSkillCount(-10);
		m_nMySp = m_nMaxSp;
		cout << "종족 스킬을 사용합니다.\n\n";
	}
	else
	{
		cout << "종족 스킬 카운트가 부족합니다. 일반 공격을 실행합니다.\n\n";
		Attack(Monsters);
	}
}

void Human::EndGrit()
{
	SetIsRaceSkill(false);
}