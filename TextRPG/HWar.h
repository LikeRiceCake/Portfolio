#ifndef _HWAR_H_
#define _HWAR_H_

#include "Warrior.h"
#include "Human.h"

class HWar : virtual public Human, virtual public Warrior
{
public:
	HWar();
	~HWar() {};

	void Fight(int nValue, Object* Monsters);
	void GritForSlash(Object* Monsters);
};

#endif