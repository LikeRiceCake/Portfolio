#ifndef _GUNNER_H_
#define _GUNNER_H_

#include "Player.h"

class Gunner : virtual public Player
{
public:
	Gunner();
	~Gunner() {};

	void ExplosionBullet(Object* Monsters);

protected:
};

#endif