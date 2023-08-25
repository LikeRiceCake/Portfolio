using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Boar");

        return Instantiate(prefab);
    }
}
