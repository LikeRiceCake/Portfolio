#include "Object.h"
#include "Tools.h"

int Object::m_nObjectNum = 0;

Object::Object()
{
	m_nObjectNum++;
	m_nMaxHp = 0;
	m_nMyHp = 0;
	m_nMoney = 0;
	m_nDamage = 0;
	m_nDeffence = 0;
	m_nMyXY = 0;
}

void Object::Attacked(int nValue)
{
	if (m_nDeffence >= nValue)
	{
		cout << "0의 데미지를 입힙니다.\n\n";
		return;
	}

	m_nMyHp -= (nValue - m_nDeffence);

	printf("%d의 데미지를 입힙니다.\n\n", nValue - m_nDeffence);
}

int Object::GetStat(_EStat_ eStat)
{
	switch (eStat)
	{
	case eHp:
		return m_nMyHp;
		break;
	case eMoney:
		return m_nMoney;
		break;
	case eXY:
		return m_nMyXY;
		break;
	default:
		return 0;
		break;
	}
}

void Object::SetMoney(int nValue)
{
	m_nMoney += nValue;
}

void Object::ReadInfo()
{
	printf("체력 : %d/%d |\n데미지 : %d | 디펜스 : %d |\n\n돈 : %d |\n\n",
		m_nMaxHp, m_nMyHp, m_nDamage, m_nDeffence, m_nMoney);
}