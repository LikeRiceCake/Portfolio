#include "Skeleton.h"
#include "Tools.h"

Skeleton::Skeleton()
{
	m_nMaxHp = 50;
	m_nDamage = 13;
	m_nDeffence = 10;
	m_nMoney = 300;
	m_nMyHp = m_nMaxHp;
	m_nMyMonsterType = eSkeleton;
}