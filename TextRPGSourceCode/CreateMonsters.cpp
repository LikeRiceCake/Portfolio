#include "Tools.h"

void CreateMonsters(Object** Objects)
{
	int i = PLAYER;

	while(i < MONSTERMAX)
	{
		int nRand = rand() % 4 + 1;

		switch (nRand)
		{
		case eZombie:
			*(Objects + i) = new Zombie;
			break;
		case eSpider:
			*(Objects + i) = new Spider;
			break;
		case eSkeleton:
			*(Objects + i) = new Skeleton;
			break;
		case eNaga:
			*(Objects + i) = new Naga;
			break;
		default:
			*(Objects + i) = new Zombie;
			break;
		}
		i++;
	}

	*(Objects + i) = new Booooss;
}