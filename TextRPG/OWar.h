#ifndef _OWAR_H_
#define _OWAR_H_

#include "Warrior.h"
#include "Orc.h"

class OWar : virtual public Orc, virtual public Warrior
{
public:
	OWar();
	~OWar() {};

	void Fight(int nValue, Object* Monsters);
	void RageForSlash(Object* Monsters);
};

#endif