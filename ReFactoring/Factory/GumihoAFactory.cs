using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoAFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Gumiho_A");

        return Instantiate(prefab);
    }
}
