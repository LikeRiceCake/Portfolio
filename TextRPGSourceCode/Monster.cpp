#include "Monster.h"
#include "Tools.h"

int Monster::m_nMonsterNum = 0;

Monster::Monster()
{
	m_nMonsterNum++;
	m_nMyMonsterType = eMonsterTypeNone;
}

Monster::~Monster()
{
	m_nMonsterNum--;
}

void Monster::Move()
{
	m_nMyXY = rand() % 12 + 1;
}

void Monster::Attack(Object* Player)
{
	int nRand = rand() % m_nDamage;

	cout << "적이 당신을 공격합니다.\n\n";

	Player->Attacked(nRand);
}

int Monster::GetMonsterNum()
{
	return m_nMonsterNum;
}

void Monster::ReadInfo()
{
	printf("타입 : %s |\n체력 : %d/%d |\n데미지 : %d | 디펜스 : %d |\n\n돈 : %d |\n\n",
		"적", m_nMaxHp, m_nMyHp, m_nDamage, m_nDeffence, m_nMoney);
}

_EMonsters_ Monster::GetType()
{
	switch (m_nMyMonsterType)
	{
	case eZombie:
		return eZombie;
	case eNaga:
		return eNaga;
	case eBooooss:
		return eBooooss;
	case eSpider:
		return eSpider;
	case eSkeleton:
		return eSkeleton;
	default:
		return eMonsterTypeNone;
	}
}