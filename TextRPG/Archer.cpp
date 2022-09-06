#include "Archer.h"
#include "Tools.h"

Archer::Archer()
{
	InitJobInfo(eArcher);
}

void Archer::BowAttack(Object* Monsters)
{
	int nRand = rand() % 15;

	if (m_nMyMp < nRand)
	{
		cout << "마력이 부족해 스킬 사용이 불가능합니다. 일반 공격을 실행합니다.\n\n";
		Attack(Monsters);
		return;
	}

	m_nMyMp -= nRand;

	nRand = rand() % m_nDamage + m_nJobSkillDamage;

	cout << "직업 스킬을 사용합니다.\n\n";

	Monsters->Attacked(nRand);
}