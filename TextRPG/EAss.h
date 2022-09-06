#ifndef _EASS_H_
#define _EASS_H_

#include "Assassin.h"
#include "Elf.h"

class EAss : virtual public Elf, virtual public Assassin
{
public:
	EAss();
	~EAss() {};

	void Fight(int nValue, Object* Monsters);
	void CriticalKnife(Object* Monsters);
};

#endif