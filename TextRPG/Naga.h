#ifndef _NAGA_H_
#define _NAGA_H_

#include "Monster.h"

class Naga : public Monster
{
public:
	Naga();
	~Naga() {};

	void operator+(Monster& Fusion);

private:

};

#endif
