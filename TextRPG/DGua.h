#ifndef _DGUA_H_
#define _DGUA_H_

#include "Guardian.h"
#include "Dwarf.h"

class DGua : virtual public Dwarf, virtual public Guardian
{
public:
	DGua();
	~DGua() {};

	void Fight(int nValue, Object* Monsters);
	void HiperShieldAttack(Object* Monsters);
};

#endif