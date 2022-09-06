#pragma once

#ifndef _TOOLS_H_
#define _TOOLS_H_

#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <conio.h>
#include <Windows.h>

#include "Enums.h"
#include "Object.h"
#include "Player.h"
#include "CreateCharacter.h"
#include "CreateMonsters.h"
#include "DeleteMemory.h"
#include "SelectJob.h"
#include "SelectRace.h"
#include "DGua.h"
#include "DGun.h"
#include "DWar.h"
#include "EArc.h"
#include "EAss.h"
#include "HArc.h"
#include "HAss.h"
#include "HGua.h"
#include "HGun.h"
#include "HWar.h"
#include "OGua.h"
#include "OWar.h"
#include "Zombie.h"
#include "Spider.h"
#include "Skeleton.h"
#include "Naga.h"
#include "Booooss.h"

#define NORMALMOB 9
#define BOSSMOB 1
#define MONSTERMAX 10
#define PLAYER 1
#define OBJECTMAX MONSTERMAX + PLAYER

#define GAMEOVER 0

using namespace std;

#endif
