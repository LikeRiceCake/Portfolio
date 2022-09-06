#ifndef _ZOMBIE_H_
#define _ZOMBIE_H_

#include "Monster.h"

class Zombie : public Monster
{
public:
	Zombie();
	~Zombie() {};

	void operator+(const char* pCheat);
	int operator==(const char* pCheat);

	friend int operator==(const char* pProper, const Zombie& pCheat);

private:
	char* m_pWeakness;
	char* m_pBody;
	char* m_pCheat;
};

#endif
