using PublicEnums.State;
using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoStat : MonsterStat
{
    protected const int GUMIHO_H_MAX_HP = 200;
    protected const int GUMIHO_A_MAX_HP = (int)(200 * 0.7f);
    protected const int GUMIHO_DAMAGE = 10;
    protected const float GUMIHO_SPEED = 4f;
    protected const float GUMIHO_SIGHT = 999f;
    protected const float GUMIHO_ATTACK_RANGE = 3.4f;
    protected const float GUMIHO_ATTACK_COOL = 5f;

    public GumihoStat(StateManager _stateManager) : base(_stateManager)
    {
        base.stateManager = _stateManager;
    }
}
