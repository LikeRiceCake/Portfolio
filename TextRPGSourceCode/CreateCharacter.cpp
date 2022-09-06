#include "Tools.h"

void CreateCharacter(Object** Objects, _ERace_ eRace, _EJob_ eJob)
{
	switch (eRace)
	{
	case eHuman:
		switch (eJob)
		{
		case eWarrior:
			*(Objects + 0) = new HWar;
			break;
		case eArcher:
			*(Objects + 0) = new HArc;
			break;
		case eAssassin:
			*(Objects + 0) = new HAss;
			break;
		case eGuardian:
			*(Objects + 0) = new HGua;
			break;
		case eGunner:
			*(Objects + 0) = new HGun;
			break;
		default:
			cout << "오류입니다. 종족과 직업을 강제합니다.\n\n";
			*(Objects + 0) = new HWar;
			break;
		}
		break;
	case eOrc:
		switch (eJob)
		{
		case eWarrior:
			*(Objects + 0) = new OWar;
			break;
		case eGuardian:
			*(Objects + 0) = new OGua;
			break;
		default:
			cout << "오류입니다. 종족과 직업을 강제합니다.\n\n";
			*(Objects + 0) = new HWar;
			break;
		}
		break;
	case eElf:
		switch (eJob)
		{
		case eArcher:
			*(Objects + 0) = new EArc;
			break;
		case eAssassin:
			*(Objects + 0) = new EAss;
			break;
		default:
			cout << "오류입니다. 종족과 직업을 강제합니다.\n\n";
			*(Objects + 0) = new HWar;
			break;
		}
		break;
	case eDwarf:
		switch (eJob)
		{
		case eWarrior:
			*(Objects + 0) = new DWar;
			break;
		case eGuardian:
			*(Objects + 0) = new DGua;
			break;
		case eGunner:
			*(Objects + 0) = new DGun;
			break;
		default:
			cout << "오류입니다. 종족과 직업을 강제합니다.\n\n";
			*(Objects + 0) = new HWar;
			break;
		}
		break;
	default:
		cout << "오류입니다. 종족과 직업을 강제합니다.\n\n";
		*(Objects + 0) = new HWar;
		break;
	}
}