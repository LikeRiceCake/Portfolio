#ifndef _GUARDIAN_H_
#define _GUARDIAN_H_

#include "Player.h"

class Guardian : virtual public Player
{
public:
	Guardian();
	~Guardian() {};

	void ShieldAttack(Object* Monsters);

protected:
};

#endif