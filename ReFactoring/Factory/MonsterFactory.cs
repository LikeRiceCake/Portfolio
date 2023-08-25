using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;

public abstract class MonsterFactory : MonoBehaviour
{
    protected ResourceManager resourceManager;

    protected GameObject prefab;

    public abstract GameObject CreateMonster();

    void Awake()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
    }
}