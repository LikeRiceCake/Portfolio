#include "Elf.h"
#include "Tools.h"

Elf::Elf()
{
	InitRaceInfo(eElf);
}

void Elf::CriticalShot(Object* Monsters)
{
	if (GetRaceSkillCount() > 2 && !GetIsRaceSkill())
	{
		SetRaceSkillCount(-2);
		SetIsRaceSkill(true);
		m_nDamage *= 2;
		cout << "종족 스킬을 사용합니다.\n\n";
	}
	else
	{
		cout << "종족 스킬 카운트가 부족합니다. 일반 공격을 실행합니다.\n\n";
		Attack(Monsters);
	}
}

void Elf::EndCriticalShot()
{
	if (GetIsRaceSkill())
	{
		SetIsRaceSkill(false);
		m_nDamage /= 2;
	}
}