using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class WolfFactory : MonsterFactory
{
    public override GameObject CreateMonster()
    {
        prefab = resourceManager.LoadMonsterPrefab("Prefabs/Monster/Wolf");

        return Instantiate(prefab);
    }
}
