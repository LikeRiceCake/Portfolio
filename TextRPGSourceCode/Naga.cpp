#include "Naga.h"
#include "Tools.h"

Naga::Naga()
{
	m_nMaxHp = 70;
	m_nDamage = 19;
	m_nDeffence = 12;
	m_nMoney = 1000;
	m_nMyHp = m_nMaxHp;
	m_nMyMonsterType = eNaga;
}

void Naga::operator+(Monster& Fusion)
{
	switch (Fusion.GetType())
	{
	case eZombie:
		cout << "퓨퓨슈슈퓨슝 나가와 좀비가 융합되었지만 이내 육체가 소실되었습니다.\n\n";
		break;
	default:
		cout << "융합이 불가능한 종족입니다.\n\n";
		break;
	}

	Zombie Test;

	int nTestNum = "Fire" == Test;
}