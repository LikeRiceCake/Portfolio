#include "Dwarf.h"
#include "Tools.h"

Dwarf::Dwarf()
{
	InitRaceInfo(eDwarf);
}

void Dwarf::HiperTechnology(Object* Monsters)
{
	if (GetRaceSkillCount() > 5 && !GetIsRaceSkill())
	{
		SetIsRaceSkill(true);
		m_nDamage += 10;
		SetRaceSkillCount(-5);
		cout << "종족 스킬을 사용합니다.\n\n";
	}
	else
	{
		cout << "종족 스킬 카운트가 부족합니다. 일반 공격을 실행합니다.\n\n";
		Attack(Monsters);
	}
}

void Dwarf::EndHiperTechnology()
{
	if (GetIsRaceSkill())
	{
		m_nDamage -= 10;
		SetIsRaceSkill(false);
	}
}