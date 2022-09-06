#ifndef _WARRIOR_H_
#define _WARRIOR_H_

#include "Player.h"

class Warrior : virtual public Player
{
public:
	Warrior();
	~Warrior() {};

	void Slash(Object* Monsters);

protected:
};

#endif