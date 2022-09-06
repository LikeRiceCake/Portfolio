#include <stdio.h>
#include <conio.h>
#include <time.h>
#include <stdlib.h>
#include <windows.h>
#include <stdbool.h>

void fSetBool(bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5], int nA, int nB, int nC);
void fSetSlots(char(*cSA)[5], char(*cSB)[5], char(*cSC)[5], bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5]);
void fResetSlots(char(*cSA)[5], char(*cSB)[5], char(*cSC)[5], bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5]);
void fPrintfNumber(char(*cSA)[5], char(*cSB)[5], char(*cSC)[5]);

#define cOnNumber 'o'
#define cOffNumber ' '
#define bOnBool true
#define bOffBool false

void main()
{
	int nSelect = 0;
	int nCharge = 0;
	int nWallet = 0;

	int i = 0;
	int nTime = 0;
	int nSlotA = 0;
	int nSlotB = 0;
	int nSlotC = 0;
	int nBet = 0;

	char cSlotA[9][5] = { ' ' };
	char cSlotB[9][5] = { ' ' };
	char cSlotC[9][5] = { ' ' };
	bool bSlotA[9][5] = { false };
	bool bSlotB[9][5] = { false };
	bool bSlotC[9][5] = { false };

	const int nEnter = 13;
	const int nSlotLimit = 9;
	const int nSlotLimitSup = 1;
	const int nMS = 5;
	const int nJackPot = 7;

	printf("\n\n\n\t\t ★슬롯 머신 게임에 오신 걸 환영합니다★\n\n");

	Sleep(1000);

	printf("\t\t☆엔터키를 누르시면 선택지를 표시합니다☆\n");

	Sleep(1000);

	printf("\t\t   ★그 외의 키를 누르시면 종료됩니다★");

	if (getch() != nEnter)
	{
		system("cls");
		printf("\n\n\n\n\n\n\t\t\t※안녕히 가십시오※");
		Sleep(3000);
		return;
	}

	nWallet = 500000;

	srand((unsigned)time(NULL));

	while (1)
	{
		fResetSlots(cSlotA, cSlotB, cSlotC, bSlotA, bSlotB, bSlotC);

		system("cls");

		printf("\n\n\n\t\t1. 슬롯 머신 시작하기 | 2. 금액 충전하기 | 3. 종료 : ");
		scanf("%d", &nSelect);

		switch (nSelect)
		{
		case 1:
			while (1)
			{
				system("cls");

				if (nWallet < 50000)
				{
					printf("베팅 금액이 부족합니다. 충전 후 이용해주세요.");

					Sleep(2000);

					break;
				}

				while (1)
				{
					printf("\n\n\n\t\t베팅 금액을 입력해주세요 (최소 5만원) [잔액 %d원] : ", nWallet);
					scanf("%d", &nBet);

					if (nBet < 50000)
					{
						printf("\n\t\t최소 금액보다 작습니다. 다시 입력해주세요");

						Sleep(2000);

						system("cls");
					}
					else if (nBet > nWallet)
					{
						printf("\n\t\t잔액보다 큽니다. 다시 입력해주세요");

						Sleep(2000);

						system("cls");
					}
					else
					{
						break;
					}
				}

				printf("\t\t레버가 돌아갈 시간을 입력해주세요 : ");
				scanf("%d", &nTime);

				if (nTime == 0)
				{
					printf("\n\t\t0이 입력되었으므로 마지막으로 나왔던 숫자로 판단합니다.\n\n");

					Sleep(2000);

					if (nSlotA == 0)
					{
						printf("\t\t현재 한 번도 슬롯을 돌리지 않아 마지막 숫자가 존재하지 않습니다. 메뉴로 돌아갑니다.");

						Sleep(2000);

						break;
					}
				}
				else
				{
					Sleep(1000);

					puts("");

					printf("\t\t\t슬롯을 돌립니다.\n\n\n");

					Sleep(2000);

					system("cls");

					i = 0;

					while (i < nTime * nMS)
					{

						fResetSlots(cSlotA, cSlotB, cSlotC, bSlotA, bSlotB, bSlotC);

						if (i < nTime * nMS / 4)
						{
							nSlotA = rand() % nSlotLimit + nSlotLimitSup;
							nSlotB = rand() % nSlotLimit + nSlotLimitSup;
							nSlotC = rand() % nSlotLimit + nSlotLimitSup;
						}
						else if (i < nTime * nMS / 2 && i > nTime * nMS / 4)
						{
							nSlotB = rand() % nSlotLimit + nSlotLimitSup;
							nSlotC = rand() % nSlotLimit + nSlotLimitSup;
						}
						else if (i > nTime * nMS / 2)
						{
							nSlotC = rand() % nSlotLimit + nSlotLimitSup;
						}

						fSetBool(bSlotA, bSlotB, bSlotC, nSlotA, nSlotB, nSlotC);
						fSetSlots(cSlotA, cSlotB, cSlotC, bSlotA, bSlotB, bSlotC);

						fPrintfNumber(cSlotA, cSlotB, cSlotC);

						Sleep(5);

						system("cls");

						i++;
					}
				}

				fPrintfNumber(cSlotA, cSlotB, cSlotC);

				Sleep(2000);

				if (nSlotA == nJackPot && nSlotB == nJackPot && nSlotC == nJackPot)
				{
					printf("\n\n\t\t★★★★★★★잭팟입니다!!!! 베팅금액의 네 배 + 1000만원을 획득합니다.★★★★★★★");
					nWallet += (nBet * 4 + 10000000);
				}
				else if (nSlotA == nSlotB && nSlotA == nSlotC && nSlotB == nSlotC)
				{
					printf("\n\n\t\t★★★★★모든 숫자가 일치합니다!! 베팅금액의 세 배 + 100만원을 획득합니다.★★★★★");
					nWallet += (nBet * 3 + 1000000);
				}
				else if (nSlotA == nSlotB || nSlotA == nSlotC || nSlotB == nSlotC)
				{
					printf("\n\n\t\t★★★두 가지의 숫자가 일치합니다! 베팅금액의 두 배 + 10만원을 획득합니다.★★★");
					nWallet += (nBet * 2 + 100000);
				}
				else
				{
					printf("\n\n\t\t  ◇일치하는 숫자가 없습니다. 베팅금액의 두 배 + 10만원을 차감합니다.◇");
					nWallet -= (nBet * 2 + 100000);
				}

				if (nWallet <= 0)
				{
					printf("\n\n\n\t\t     §모든 현금을 잃으셨습니다. 게임을 종료합니다.§");

					Sleep(2000);

					return;
				}

				printf("\n\n\t\t\t1. 메뉴 호출 | 그 외. 재도전 : ");
				scanf("%d", &nSelect);

				if (nSelect == 1)
				{
					break;
				}
			}
			break;
		case 2:
			system("cls");

			while (1)
			{
				printf("\n\n\n\t\t충전하실 금액을 적어주세요 (최소 5만원) [잔액 %d원] : ", nWallet);
				scanf("%d", &nCharge);

				if (nCharge < 50000)
				{
					printf("\n\t\t최소 금액보다 작습니다. 다시 입력해주세요");

					Sleep(2000);

					system("cls");
				}
				else
				{
					break;
				}
			}

			nWallet += nCharge;

			Sleep(1000);

			printf("\n\t\t%d원 충전 완료됐습니다.\n", nCharge);
			printf("\t\t현재 잔액 : %d원\n\n", nWallet);

			printf("\t\t메인 메뉴로 돌아갑니다.");

			Sleep(3500);

			break;
		case 3:
			system("cls");
			printf("\n\n\n\n\n\n\t\t\t※안녕히 가십시오※");
			Sleep(3000);
			return;
			return;
			break;
		}
	}
}

void fSetBool(bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5], int nA, int nB, int nC)
{
	switch (nA)
	{
	case 1:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		break;
	case 2:
		for (int i = 0; i < 5; i++)
		{
			bSA[0][i] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 4; i++)
		{
			bSA[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSA[i][0] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSA[8][i] = true;
		}
		break;
	case 3:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSA[i][j] = true;
			}
		}
		break;
	case 4:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[4][i] = true;
		}
		break;
	case 5:
		for (int i = 0; i < 5; i++)
		{
			bSA[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[8][i] = true;
		}
		break;
	case 6:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][0] = true;
		}
		for (int i = 4; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSA[i][j] = true;
			}
		}
		for (int i = 4; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		break;
	case 7:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[i][0] = true;
		}
		break;
	case 8:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 9; i++)
		{
			bSA[i][0] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSA[i][j] = true;
			}
		}
		break;
	case 9:
		for (int i = 0; i < 9; i++)
		{
			bSA[i][4] = true;
		}
		for (int i = 0; i < 5; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSA[i][j] = true;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			bSA[i][0] = true;
		}
		break;
	}
	switch (nB)
	{
	case 1:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		break;
	case 2:
		for (int i = 0; i < 5; i++)
		{
			bSB[0][i] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 4; i++)
		{
			bSB[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSB[i][0] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSB[8][i] = true;
		}
		break;
	case 3:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSB[i][j] = true;
			}
		}
		break;
	case 4:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[4][i] = true;
		}
		break;
	case 5:
		for (int i = 0; i < 5; i++)
		{
			bSB[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[8][i] = true;
		}
		break;
	case 6:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][0] = true;
		}
		for (int i = 4; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSB[i][j] = true;
			}
		}
		for (int i = 4; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		break;
	case 7:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[i][0] = true;
		}
		break;
	case 8:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 9; i++)
		{
			bSB[i][0] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSB[i][j] = true;
			}
		}
		break;
	case 9:
		for (int i = 0; i < 9; i++)
		{
			bSB[i][4] = true;
		}
		for (int i = 0; i < 5; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSB[i][j] = true;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			bSB[i][0] = true;
		}
		break;
	}
	switch (nC)
	{
	case 1:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		break;
	case 2:
		for (int i = 0; i < 5; i++)
		{
			bSC[0][i] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 4; i++)
		{
			bSC[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSC[i][0] = true;
		}
		for (int i = 1; i < 5; i++)
		{
			bSC[8][i] = true;
		}
		break;
	case 3:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSC[i][j] = true;
			}
		}
		break;
	case 4:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[4][i] = true;
		}
		break;
	case 5:
		for (int i = 0; i < 5; i++)
		{
			bSC[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[i][0] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[4][i] = true;
		}
		for (int i = 5; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[8][i] = true;
		}
		break;
	case 6:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][0] = true;
		}
		for (int i = 4; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSC[i][j] = true;
			}
		}
		for (int i = 4; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		break;
	case 7:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[0][i] = true;
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[i][0] = true;
		}
		break;
	case 8:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 9; i++)
		{
			bSC[i][0] = true;
		}
		for (int i = 0; i < 9; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSC[i][j] = true;
			}
		}
		break;
	case 9:
		for (int i = 0; i < 9; i++)
		{
			bSC[i][4] = true;
		}
		for (int i = 0; i < 5; i += 4)
		{
			for (int j = 0; j < 5; j++)
			{
				bSC[i][j] = true;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			bSC[i][0] = true;
		}
		break;
	}
}

void fSetSlots(char(*cSA)[5], char(*cSB)[5], char(*cSC)[5], bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5])
{
	for (int i = 0; i < 9; i++)
	{
		for (int j = 0; j < 5; j++)
		{
			if (bSA[i][j] == true)
			{
				cSA[i][j] = cOnNumber;
			}
			if (bSB[i][j] == true)
			{
				cSB[i][j] = cOnNumber;
			}
			if (bSC[i][j] == true)
			{
				cSC[i][j] = cOnNumber;
			}
		}
	}
}

void fResetSlots(char(*cSA)[5], char(*cSB)[5], char(*cSC)[5], bool(*bSA)[5], bool(*bSB)[5], bool(*bSC)[5])
{
	for (int n = 0; n < 9; n++)
	{
		for (int m = 0; m < 5; m++)
		{
			cSA[n][m] = cOffNumber;
			cSB[n][m] = cOffNumber;
			cSC[n][m] = cOffNumber;
			bSA[n][m] = false;
			bSB[n][m] = false;
			bSC[n][m] = false;
		}
	}
}

void fPrintfNumber(char(*cSlotA)[5], char(*cSlotB)[5], char(*cSlotC)[5])
{
	printf("\n\n\t\tooooooooooooooooooooooooooooooooooooo\n");
	printf("\t\to                                   o\n");
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[0][0], cSlotA[0][1], cSlotA[0][2], cSlotA[0][3], cSlotA[0][4], cSlotB[0][0], cSlotB[0][1], cSlotB[0][2], cSlotB[0][3], cSlotB[0][4], cSlotC[0][0], cSlotC[0][1], cSlotC[0][2], cSlotC[0][3], cSlotC[0][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[1][0], cSlotA[1][1], cSlotA[1][2], cSlotA[1][3], cSlotA[1][4], cSlotB[1][0], cSlotB[1][1], cSlotB[1][2], cSlotB[1][3], cSlotB[1][4], cSlotC[1][0], cSlotC[1][1], cSlotC[1][2], cSlotC[1][3], cSlotC[1][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[2][0], cSlotA[2][1], cSlotA[2][2], cSlotA[2][3], cSlotA[2][4], cSlotB[2][0], cSlotB[2][1], cSlotB[2][2], cSlotB[2][3], cSlotB[2][4], cSlotC[2][0], cSlotC[2][1], cSlotC[2][2], cSlotC[2][3], cSlotC[2][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[3][0], cSlotA[3][1], cSlotA[3][2], cSlotA[3][3], cSlotA[3][4], cSlotB[3][0], cSlotB[3][1], cSlotB[3][2], cSlotB[3][3], cSlotB[3][4], cSlotC[3][0], cSlotC[3][1], cSlotC[3][2], cSlotC[3][3], cSlotC[3][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[4][0], cSlotA[4][1], cSlotA[4][2], cSlotA[4][3], cSlotA[4][4], cSlotB[4][0], cSlotB[4][1], cSlotB[4][2], cSlotB[4][3], cSlotB[4][4], cSlotC[4][0], cSlotC[4][1], cSlotC[4][2], cSlotC[4][3], cSlotC[4][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[5][0], cSlotA[5][1], cSlotA[5][2], cSlotA[5][3], cSlotA[5][4], cSlotB[5][0], cSlotB[5][1], cSlotB[5][2], cSlotB[5][3], cSlotB[5][4], cSlotC[5][0], cSlotC[5][1], cSlotC[5][2], cSlotC[5][3], cSlotC[5][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[6][0], cSlotA[6][1], cSlotA[6][2], cSlotA[6][3], cSlotA[6][4], cSlotB[6][0], cSlotB[6][1], cSlotB[6][2], cSlotB[6][3], cSlotB[6][4], cSlotC[6][0], cSlotC[6][1], cSlotC[6][2], cSlotC[6][3], cSlotC[6][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[7][0], cSlotA[7][1], cSlotA[7][2], cSlotA[7][3], cSlotA[7][4], cSlotB[7][0], cSlotB[7][1], cSlotB[7][2], cSlotB[7][3], cSlotB[7][4], cSlotC[7][0], cSlotC[7][1], cSlotC[7][2], cSlotC[7][3], cSlotC[7][4]);
	printf("\t\to\t%c%c%c%c%c   %c%c%c%c%c   %c%c%c%c%c       o\n",
		cSlotA[8][0], cSlotA[8][1], cSlotA[8][2], cSlotA[8][3], cSlotA[8][4], cSlotB[8][0], cSlotB[8][1], cSlotB[8][2], cSlotB[8][3], cSlotB[8][4], cSlotC[8][0], cSlotC[8][1], cSlotC[8][2], cSlotC[8][3], cSlotC[8][4]);
	printf("\t\to                                   o\n");
	printf("\t\tooooooooooooooooooooooooooooooooooooo\n");
}