using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoIdleFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Gumiho_Idle");

        return Instantiate(prefab);
    }
}
