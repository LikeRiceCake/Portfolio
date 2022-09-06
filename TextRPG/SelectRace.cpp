#include "Tools.h"

void SelectRace(_ERace_* eRace)
{
	scanf("%d", eRace);

	switch (*eRace)
	{
	case eHuman:
	case eOrc:
	case eElf:
	case eDwarf:
		break;
	default:
		cout << "입력 오류입니다. 종족을 휴먼으로 강제합니다.\n\n";
		*eRace = eHuman;
		break;
	}
}