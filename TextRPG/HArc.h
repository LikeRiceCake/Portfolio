#ifndef _HARC_H_
#define _HARC_H_

#include "Archer.h"
#include "Human.h"

class HArc : virtual public Human, virtual public Archer
{
public:
	HArc();
	~HArc() {};

	void Fight(int nValue, Object* Monsters);
	void GritForBowAttack(Object* Monsters);
};

#endif