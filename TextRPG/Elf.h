#ifndef _ELF_H_
#define _ELF_H_

#include "Player.h"

class Elf : virtual public Player
{
public:
	Elf();
	~Elf() {};

	void CriticalShot(Object* Monsters);
	void EndCriticalShot();

protected:
};

#endif