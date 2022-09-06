#ifndef _HGUN_H
#define _HGUN_H

#include "Gunner.h"
#include "Human.h"

class HGun : virtual public Human, virtual public Gunner
{
public:
	HGun();
	~HGun() {};

	void Fight(int nValue, Object* Monsters);
	void GritForExplosionBullet(Object* Monsters);
};

#endif