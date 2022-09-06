#include "HArc.h"
#include "Tools.h"

HArc::HArc()
{
	m_nMaxHp = 100;
	m_nMaxSp = 50;
	m_nMaxMp = 70;
	m_nDamage = 15;
	m_nDeffence = 10;
	m_nJobSkillDamage = 30;
	m_nMyHp = m_nMaxHp;
	m_nMySp = m_nMaxSp;
	m_nMyMp = m_nMaxMp;
}

void HArc::Fight(int nValue, Object* Monsters)
{
	switch (nValue)
	{
	case eAttack:
		Attack(Monsters);
		break;
	case eRaceSkill:
		UseGrit(Monsters);
		break;
	case eJobSkill:
		BowAttack(Monsters);
		break;
	case eUniqueSkill:
		GritForBowAttack(Monsters);
		break;
	default:
		cout << "오류입니다.\n\n";
		break;
	}
}

void HArc::GritForBowAttack(Object* Monsters)
{
	int nRand1 = rand() % 10;
	int nRand2 = rand() % 50;


	if (m_nMySp < nRand1 || m_nMyMp < nRand2)
	{
		cout << "기력 또는 마력이 부족해 스킬 사용이 불가능합니다.\n\n";
		Attack(Monsters);
		return;
	}

	m_nMySp -= nRand1;
	m_nMyMp -= nRand2;

	int nRand3 = rand() % m_nDamage + (m_nJobSkillDamage * 2);

	cout << "고유 스킬을 사용합니다.\n\n";

	Monsters->Attacked(nRand3);
}