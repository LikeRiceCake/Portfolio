#include "Spider.h"
#include "Tools.h"

Spider::Spider()
{
	m_nMaxHp = 30;
	m_nDamage = 7;
	m_nDeffence = 3;
	m_nMoney = 150;
	m_nMyHp = m_nMaxHp;
	m_nMyMonsterType = eSpider;
}