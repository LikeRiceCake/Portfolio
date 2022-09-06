#include "Zombie.h"
#include "Tools.h"

Zombie::Zombie()
{
	m_nMaxHp = 20;
	m_nDamage = 5;
	m_nDeffence = 1;
	m_nMoney = 50;
	m_nMyHp = m_nMaxHp;
	m_pBody = new char[strlen("불에타는 신체") + 1];
	strcpy(m_pBody ,"불에타는 신체");
	m_pWeakness = new char[strlen("Fire") + 1];
	strcpy(m_pWeakness, "Fire");
	m_pCheat = 0;
	m_nMyMonsterType = eZombie;
}

void Zombie::operator+(const char* pCheat)
{
	if (m_pCheat != NULL)
	{
		delete[]m_pCheat;
		m_pCheat = 0;
	}

	Zombie TestCheat;

	TestCheat.m_pCheat = new char[strlen(pCheat)];
	strcpy(TestCheat.m_pCheat, pCheat);

	if ("Fire" == TestCheat || TestCheat == pCheat)
	{
		cout << "좀비가 불을 보고 겁을 집어먹습니다!!\n\n";
	}
	else if ("Ice" == TestCheat || TestCheat == pCheat)
	{
		cout << "좀비가 얼음을 보며 신기해합니다.\n\n";
	}
	else
		cout << "좀비가 아무 반응도 보이지 않습니다.\n\n";
}

int operator==(const char* pProper, const Zombie& pCheat)
{
	if (strcmp(pProper, pCheat.m_pCheat) == 0)
	{
		return 1;
	}
	return 0;
}

int Zombie::operator==(const char* pCheat)
{
	if (strcmp(pCheat, m_pWeakness) == 0)
	{
		return 1;
	}
	return 0;
}