#ifndef _HUMAN_H_
#define _HUMAN_H_

#include "Player.h"

class Human : virtual public Player
{
public:
	Human();
	~Human() {};

	void UseGrit(Object* Monsters);
	void EndGrit();

protected:
};

#endif