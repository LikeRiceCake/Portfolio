using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Bird");

        return Instantiate(prefab);
    }
}
