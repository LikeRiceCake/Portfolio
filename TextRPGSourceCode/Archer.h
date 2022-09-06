#ifndef _ARCHER_H_
#define _ARCHER_H_

#include "Player.h"

class Archer : virtual public Player
{
public:
	Archer();
	~Archer() {};

	void BowAttack(Object* Monsters);

protected:
};

#endif