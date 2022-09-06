#ifndef _DWAR_H_
#define _DWAR_H_

#include "Warrior.h"
#include "Dwarf.h"

class DWar : virtual public Dwarf, virtual public Warrior
{
public:
	DWar();
	~DWar() {};

	void Fight(int nValue, Object* Monsters);
	void HiperSlash(Object* Monsters);
};

#endif