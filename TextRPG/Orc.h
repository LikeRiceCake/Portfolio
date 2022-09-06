#ifndef _ORC_H_
#define _ORC_H_

#include "Player.h"

class Orc : virtual public Player
{
public:
	Orc();
	~Orc() {};

	void Rage(Object* Monsters);
	void EndRage();

protected:
};

#endif