#ifndef _MONSTES_H_
#define _MONSTES_H_

#include "Object.h"

class Monster : public Object
{
public:
	Monster();
	~Monster();

	void Move();
	void Attack(Object* Player);
	int GetMonsterNum();
	void ReadInfo();
	_EMonsters_ GetType();

protected:
	static int m_nMonsterNum;
	_EMonsters_ m_nMyMonsterType;
};

#endif
