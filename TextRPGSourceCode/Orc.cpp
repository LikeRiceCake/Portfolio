#include "Orc.h"
#include "Tools.h"

Orc::Orc()
{
	InitRaceInfo(eOrc);
}

void Orc::Rage(Object* Monsters)
{
	if (GetRaceSkillCount() > 100 && !GetIsRaceSkill())
	{
		SetIsRaceSkill(true);
		SetRaceSkillCount(-100);
		m_nDeffence += 15;
		cout << "종족 스킬을 사용합니다.\n\n";
	}
	else
	{
		cout << "종족 스킬 카운트가 부족합니다. 일반 공격을 실행합니다.\n\n";
		Attack(Monsters);
	}
}

void Orc::EndRage()
{
	m_nDeffence -= 15;
	SetIsRaceSkill(false);
}