#ifndef _ASSASSIN_H_
#define _ASSASSIN_H_

#include "Player.h"

class Assassin : virtual public Player
{
public:
	Assassin();
	~Assassin() {};

	void ThrowKnife(Object* Monsters);

protected:
};

#endif