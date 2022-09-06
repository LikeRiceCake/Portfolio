#include "DeleteMemory.h"

void DeleteMemory(Object** Objects)
{
	for (int i = 0; i < OBJECTMAX; i++)
	{
		if (*(Objects + i) != NULL)
		{
			delete *(Objects + i);
			*(Objects + i) = 0;
		}
	}
}