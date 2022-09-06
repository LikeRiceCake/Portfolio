#ifndef _HGUA_H_
#define _HGUA_H_

#include "Guardian.h"
#include "Human.h"

class HGua : virtual public Human, virtual public Guardian
{
public:
	HGua();
	~HGua() {};

	void Fight(int nValue, Object* Monsters);
	void GritForShieldAttack(Object* Monsters);
};

#endif