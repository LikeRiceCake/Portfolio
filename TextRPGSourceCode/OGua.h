#ifndef _OGUA_H_
#define _OGUA_H_

#include "Guardian.h"
#include "Orc.h"

class OGua : virtual public Orc, virtual public Guardian
{
public:
	OGua();
	~OGua() {};

	void Fight(int nValue, Object* Monsters);
	void RageForShieldAttack(Object* Monsters);
};

#endif