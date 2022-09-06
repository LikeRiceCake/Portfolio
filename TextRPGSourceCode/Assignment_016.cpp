#include "Tools.h"
//#define _TEST16_

int main()
{
	srand((unsigned)time(NULL));

	_ERace_ eRace = eRaceNone;
	_EJob_ eJob = eJobNone;
	_EStat_ eStat = eStatNone;
	_EFight_ eFight = eAttack;

	Object* Objects[MONSTERMAX + PLAYER];

	int nTargetIndex = 0;
	bool bIsMeet = false;
	char cChooseAct = '\0';

	cout << "당신의 종족을 선택해주세요.\n";
	cout << "1는 휴먼, 2는 오크, 3는 엘프, 4는 드워프입니다.\n\n";


	SelectRace(&eRace);


	printf("선택됐습니다.\n\n");

	cout << "당신의 직업을 선택해주세요.\n";
	cout << "1은 워리어, 2는 아처, 3은 어쌔신, 4는 가디언, 5는 거너입니다.\n\n";

	cout << "\t\t\t\t\t※※직업선택 전 주의※※\n";
	cout << "휴먼은 모든 직업이 가능 | 오크는 워리어, 가디언 가능 | 엘프는 아처, 어쌔신 가능 | 드워프는 워리어, 가디언, 거너 가능 |\n\n";


	SelectJob(eRace, &eJob);


	printf("선택됐습니다.\n\n");

	cout << "캐릭터를 생성합니다.\n\n";


	CreateCharacter(Objects, eRace, eJob);


	cout << "캐릭터가 생성되었습니다.\n\n";

	cout << "몬스터를 생성합니다.\n\n";


	CreateMonsters(Objects);


	cout << "몬스터가 생성되었습니다.\n\n";

	cout << "게임에 입장합니다.\n\n";

	Sleep(2000);

	while (1)
	{
		system("cls");

		Objects[0]->ReadInfo();

		cout << "m을 입력하면 이동합니다, r키를 입력하면 휴식합니다.\n\n";

		scanf("%c", &cChooseAct);

		switch (cChooseAct)
		{
		case 'm':
			if (Objects[0]->GetStat(eSp) < 5)
			{
				dynamic_cast<Player*>(Objects[0])->Rest();
				break;
			}
			else
			{
				if (dynamic_cast<Monster*>(Objects[MONSTERMAX])->GetMonsterNum() == 1)
				{
					bIsMeet = true;
					nTargetIndex = MONSTERMAX;
				}
				else
				{
					for (int i = NORMALMOB; i >= 0; i--)
					{
						if (Objects[i] != NULL)
						{
							Objects[i]->Move();

							if (i != 0 && !bIsMeet && Objects[0]->GetStat(eXY) == Objects[i]->GetStat(eXY))
							{
								bIsMeet = true;
								nTargetIndex = i;
							}
						}
					}
				}
				if (bIsMeet)
				{
					cout << "적과 조우합니다. 싸움을 시작합니다.\n\n";

					getch();

					while (1)
					{
						system("cls");

						Objects[0]->ReadInfo();

						Objects[nTargetIndex]->ReadInfo();

#ifdef _TEST16_
						if (dynamic_cast<Monster*>(Objects[nTargetIndex])->GetType() == eZombie)
						{
							char pCheat[5];

							printf("치트키 입력 : ");
							scanf("%s", pCheat);

							*dynamic_cast<Zombie*>(Objects[nTargetIndex]) + pCheat;

							Naga TestNaga;

							TestNaga + *dynamic_cast<Zombie*>(Objects[nTargetIndex]);
						}
#endif

						printf("공격 방식을 선택해주세요.\n1 : 일반 공격 | 2 : 종족 스킬 | 3 : 직업스킬 | 4 : 고유스킬\n");

						scanf("%d", &eFight);

						dynamic_cast<Player*>(Objects[0])->Fight(eFight, Objects[nTargetIndex]);

						if (Objects[nTargetIndex]->GetStat(eHp) < 0)
						{
							cout << "적이 쓰러집니다. 골드를 획득합니다.";

							Objects[0]->SetMoney(Objects[nTargetIndex]->GetStat(eMoney));

							delete Objects[nTargetIndex];
							Objects[nTargetIndex] = 0;

							bIsMeet = false;
							nTargetIndex = 0;

							if (dynamic_cast<Player*>(Objects[0])->GetIsRaceSkill())
							{
								switch (eRace)
								{
								case eHuman:
									dynamic_cast<Human*>(Objects[0])->EndGrit();
									break;
								case eOrc:
									dynamic_cast<Orc*>(Objects[0])->EndRage();
									break;
								case eElf:
									dynamic_cast<Elf*>(Objects[0])->EndCriticalShot();
									break;
								case eDwarf:
									dynamic_cast<Dwarf*>(Objects[0])->EndHiperTechnology();
									break;
								}
							}

							if (Objects[MONSTERMAX] == NULL)
							{
								cout << "모든 몬스터를 토벌했습니다. 게임에서 승리합니다.\n\n";

								DeleteMemory(Objects);

								return GAMEOVER;
							}

							getch();

							break;
						}

						getch();

						Objects[nTargetIndex]->Attack(Objects[0]);

						dynamic_cast<Player*>(Objects[0])->SetRaceSkillCount(2);

						if (Objects[0]->GetStat(eHp) < 0)
						{
							cout << "당신은 쓰러졌습니다. 게임에서 패배합니다.\n\n";

							DeleteMemory(Objects);

							return GAMEOVER;
						}

						getch();
					}
				}
				else
				{
					cout << "아무일도 일어나지 않았습니다.\n\n";

					getch();
				}
				break;
			}
		case 'r':
			dynamic_cast<Player*>(Objects[0])->Rest();
			cout << "휴식합니다.\n\n";
			getch();
			break;
		default:
			break;
		}
	}
}