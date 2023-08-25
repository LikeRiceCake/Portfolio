using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeoghoFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Heogho");

        return Instantiate(prefab);
    }
}
