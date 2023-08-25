using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumihoHFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Gumiho_H");

        return Instantiate(prefab);
    }
}
