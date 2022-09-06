#ifndef _HASS_H_
#define _HASS_H_

#include "Assassin.h"
#include "Human.h"

class HAss : virtual public Human, virtual public Assassin
{
public:
	HAss();
	~HAss() {};

	void Fight(int nValue, Object* Monsters);
	void GritForThrowKnife(Object* Monsters);
};

#endif