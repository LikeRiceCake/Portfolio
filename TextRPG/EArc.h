#ifndef _EARC_H_
#define _EARC_H_

#include "Archer.h"
#include "Elf.h"

class EArc : virtual public Elf, virtual public Archer
{
public:
	EArc();
	~EArc() {};

	void Fight(int nValue, Object* Monsters);
	void CriticalBow(Object* Monsters);
};

#endif