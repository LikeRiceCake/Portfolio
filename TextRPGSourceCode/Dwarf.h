#ifndef _DWARF_H_
#define _DWARF_H_

#include "Player.h"

class Dwarf : virtual public Player
{
public:
	Dwarf();
	~Dwarf() {};

	void HiperTechnology(Object* Monsters);
	void EndHiperTechnology();

protected:
};

#endif