#include "Tools.h"

void SelectJob(_ERace_ eRace, _EJob_* eJob)
{
	scanf("%d", eJob);

	switch (*eJob)
	{
	case eWarrior:
		switch (eRace)
		{
		case eElf:
			cout << "불가능한 직업입니다. 직업을 아처로 강제합니다.\n\n";
			*eJob = eArcher;
			break;
		default:
			break;
		}
		break;
	case eArcher:
		switch (eRace)
		{
		case eOrc:
		case eDwarf:
			cout << "불가능한 직업입니다. 직업을 워리어로 강제합니다.\n\n";
			*eJob = eWarrior;
			break;
		default:
			break;
		}
		break;
	case eAssassin:
		switch (eRace)
		{
		case eOrc:
		case eDwarf:
			cout << "불가능한 직업입니다. 직업을 워리어로 강제합니다.\n\n";
			*eJob = eWarrior;
			break;
		default:
			break;
		}
		break;
	case eGuardian:
		switch (eRace)
		{
		case eElf:
			cout << "불가능한 직업입니다. 직업을 아처로 강제합니다.\n\n";
			*eJob = eArcher;
			break;
		default:
			break;
		}
		break;
	case eGunner:
		switch (eRace)
		{
		case eOrc:
			cout << "불가능한 직업입니다. 직업을 워리어로 강제합니다.\n\n";
			*eJob = eWarrior;
			break;
		case eElf:
			cout << "불가능한 직업입니다. 직업을 아처로 강제합니다.\n\n";
			*eJob = eArcher;
			break;
		default:
			break;
		}
		break;
	default:
		cout << "입력 오류입니다. 직업을 강제합니다.\n\n";
		switch (eRace)
		{
		case eHuman:
			*eJob = eWarrior;
			break;
		case eOrc:
			*eJob = eWarrior;
			break;
		case eElf:
			*eJob = eArcher;
			break;
		case eDwarf:
			*eJob = eWarrior;
			break;
		}
		break;;
	}
}