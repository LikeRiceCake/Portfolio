#ifndef _OBJECT_H_
#define _OBJECT_H_

#include "Enums.h"

class Object
{
public:
	Object();
	virtual ~Object() {};

	virtual void Move() {};
	virtual void Attack(Object* Monsters) {};
	void Attacked(int nValue);
	virtual int GetStat(_EStat_ eStat);
	void SetMoney(int nValue);
	virtual void ReadInfo();

protected:
	static int m_nObjectNum;
	int m_nMaxHp;
	int m_nMyHp;
	int m_nMoney;
	int m_nDamage;
	int m_nDeffence;
	int m_nMyXY;
};

#endif