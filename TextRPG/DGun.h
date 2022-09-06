#ifndef _DGUN_H_
#define _DGUN_H_

#include "Gunner.h"
#include "Dwarf.h"

class DGun : virtual public Dwarf, virtual public Gunner
{
public:
	DGun();
	~DGun() {};

	void Fight(int nValue, Object* Monsters);
	void HiperBullet(Object* Monsters);
};

#endif