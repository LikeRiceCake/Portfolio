#include "Player.h"
#include "Tools.h"

Player::Player()
{
	m_nMaxSp = 0;
	m_nMySp = 0;
	m_nMaxMp = 0;
	m_nMyMp = 0;
	m_nMyRace = 0;
	m_nMyJob = 0;
	m_nJobSkillDamage = 0;
	m_nRaceSkillCount = 0;
	m_bIsRaceSkill = false;
}

Player::~Player()
{
	delete[]m_nMyRace;
	m_nMyRace = 0;
	delete[]m_nMyJob;
	m_nMyJob = 0;
}

void Player::Rest()
{
	int nRand = 0;

	nRand = rand() % 10;
	m_nMyHp += nRand;
	if (m_nMyHp > m_nMaxHp)
		m_nMyHp = m_nMaxHp;

	nRand = rand() % 10;
	m_nMyMp += nRand;
	if (m_nMyMp > m_nMaxMp)
		m_nMyMp = m_nMaxMp;

	nRand = rand() % 10;
	m_nMySp += nRand;
	if (m_nMySp > m_nMaxSp)
		m_nMySp = m_nMaxSp;
}

void Player::Attack(Object* Monsters)
{
	int nRand = rand() % 10;

	if (m_nMySp < nRand)
	{
		cout << "기력이 없어 휴식을 하고 턴을 종료합니다.\n\n";
		Rest();
		return;
	}

	m_nMySp -= nRand;

	nRand = rand() % m_nDamage + 5;

	cout << "당신이 적을 공격합니다.\n\n";

	Monsters->Attacked(nRand);
}

void Player::Fight(int nValue, Object* Monsters)
{
	switch (nValue)
	{
	case eAttack:
		Attack(Monsters);
		break;
	default:
		break;
	}
}

void Player::Move()
{
	m_nMySp -= rand() % 5;

	m_nMyXY = rand() % 12 + 1;
}

int Player::GetStat(_EStat_ eStat)
{
	switch (eStat)
	{
	case eHp:
		return m_nMyHp;
		break;
	case eSp:
		return m_nMySp;
		break;
	case eMp:
		return m_nMyMp;
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

void Player::ReadInfo()
{
	printf("종족 : %s | 직업 : %s |\n체력 : %d/%d | 기력 : %d/%d | 마력 : %d/%d |\n데미지 : %d | 디펜스 : %d |\n종족 스킬 카운트 : %d | 직업 스킬 데미지 : %d |\n",
		m_nMyRace, m_nMyJob, m_nMaxHp, m_nMyHp, m_nMaxSp, m_nMySp, m_nMaxMp, m_nMyMp, m_nDamage, m_nDeffence, m_nRaceSkillCount, m_nJobSkillDamage);

	cout << boolalpha << "종족 스킬 온 : " << m_bIsRaceSkill << "\n\n";

	printf("돈 : %d\n\n", m_nMoney);
}

void Player::InitRaceInfo(int nRaceValue)
{
	switch (nRaceValue)
	{
	case eHuman:
		m_nMyRace = new char[strlen("휴먼") + 1];
		strcpy(m_nMyRace, "휴먼");
		break;
	case eOrc:
		m_nMyRace = new char[strlen("오크") + 1];
		strcpy(m_nMyRace, "오크");
		break;
	case eElf:
		m_nMyRace = new char[strlen("엘프") + 1];
		strcpy(m_nMyRace, "엘프");
		break;
	case eDwarf:
		m_nMyRace = new char[strlen("드워프") + 1];
		strcpy(m_nMyRace, "드워프");
		break;
	default:
		m_nMyRace = new char[strlen("알수없음") + 1];
		strcpy(m_nMyRace, "알수없음");
		break;
	}
}

void Player::InitJobInfo(int nJobValue)
{
	switch (nJobValue)
	{
	case eWarrior:
		m_nMyJob = new char[strlen("워리어") + 1];
		strcpy(m_nMyJob, "워리어");
		break;
	case eArcher:
		m_nMyJob = new char[strlen("아처") + 1];
		strcpy(m_nMyJob, "아처");
		break;
	case eAssassin:
		m_nMyJob = new char[strlen("어쌔신") + 1];
		strcpy(m_nMyJob, "어쌔신");
		break;
	case eGuardian:
		m_nMyJob = new char[strlen("가디언") + 1];
		strcpy(m_nMyJob, "가디언");
		break;
	case eGunner:
		m_nMyJob = new char[strlen("거너") + 1];
		strcpy(m_nMyJob, "거너");
		break;
	default:
		m_nMyJob = new char[strlen("알수없음") + 1];
		strcpy(m_nMyJob, "알수없음");
		break;
	}
}

int Player::GetRaceSkillCount()
{
	return m_nRaceSkillCount;
}

bool Player::GetIsRaceSkill()
{
	return m_bIsRaceSkill;
}

void Player::SetRaceSkillCount(int nValue)
{
	m_nRaceSkillCount += nValue;
}

void Player::SetIsRaceSkill(bool bValue)
{
	m_bIsRaceSkill = bValue;
}