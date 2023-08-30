using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums;

public class MonsterSpawner : MonoBehaviour
{
    MonsterFactory[] myFactories;

    GameObject[][] myMonsters;

    GameObject[][] spawnPos;

    DieManager dieManager;

    void Start()
    {
        dieManager = GameObject.Find("Manager").GetComponent<DieManager>();

        myFactories = new MonsterFactory[(int)_EMonsterType_.eMax];
        myMonsters = new GameObject[(int)_ESpawnStageType_.esstMax][];
        spawnPos = new GameObject[(int)_ESpawnStageType_.esstMax][];

        for (int i = 0; i < (int)_ESpawnStageType_.esstMax; i++)
        {
            spawnPos[i] = new GameObject[GameObject.Find(((_ESpawnStageType_)i).ToString()).transform.childCount];
            for (int j = 0; j < spawnPos[i].Length; j++)
                spawnPos[i][j] = GameObject.Find(((_ESpawnStageType_)i).ToString()).transform.GetChild(j).gameObject;
        }

        for (int i = 0; i < (int)_ESpawnStageType_.esstMax; i++)
            myMonsters[i] = new GameObject[spawnPos[i].Length];

        myFactories[(int)_EMonsterType_.emtBoar] = gameObject.AddComponent<BoarFactory>();
        myFactories[(int)_EMonsterType_.emtBird] = gameObject.AddComponent<BirdFactory>();
        myFactories[(int)_EMonsterType_.emtWolf] = gameObject.AddComponent<WolfFactory>();
        myFactories[(int)_EMonsterType_.emtGumihoH] = gameObject.AddComponent<GumihoHFactory>();
        myFactories[(int)_EMonsterType_.emtGumihoA] = gameObject.AddComponent<GumihoAFactory>();
        myFactories[(int)_EMonsterType_.emtGumihoIdle] = gameObject.AddComponent<GumihoIdleFactory>();
        myFactories[(int)_EMonsterType_.emtHeogho] = gameObject.AddComponent<HeoghoFactory>();
    }

    public void CreateMonsters()
    {
        for (int i = 0; i < (int)_ESpawnStageType_.esstMax; i++)
        {
            for (int j = 0; j < spawnPos[i].Length; j++)
            {
                switch (spawnPos[i][j].tag)
                {
                    case "BoarSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtBoar].CreateMonster();
                        break;
                    case "BirdSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtBird].CreateMonster();
                        break;
                    case "WolfSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtWolf].CreateMonster();
                        break;
                    case "GumihoHSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtGumihoH].CreateMonster();
                        break;
                    case "GumihoASpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtGumihoA].CreateMonster();
                        break;
                    case "GumihoIdleSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtGumihoIdle].CreateMonster();
                        break;
                    case "HeoghoSpawn":
                        myMonsters[i][j] = myFactories[(int)_EMonsterType_.emtHeogho].CreateMonster();
                        break;
                }
                if (myMonsters[i][j].GetComponent<IDieSubject>() != null)
                    dieManager.AddMyFunc(myMonsters[i][j].GetComponent<IDieSubject>());
                myMonsters[i][j].SetActive(false);
            }
        }
    }

    public void CreateMonsters(_ESpawnStageType_ _type)
    {
        for (int i = 0; i < spawnPos[(int)_type].Length; i++)
        {
            switch (spawnPos[(int)_type][i].tag)
            {
                case "BoarSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtBoar].CreateMonster();
                    break;
                case "BirdSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtBird].CreateMonster();
                    break;
                case "WolfSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtWolf].CreateMonster();
                    break;
                case "GumihoHSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtGumihoH].CreateMonster();
                    break;
                case "GumihoASpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtGumihoA].CreateMonster();
                    break;
                case "GumihoIdleSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtGumihoIdle].CreateMonster();
                    break;
                case "HeoghoSpawn":
                    myMonsters[(int)_type][i] = myFactories[(int)_EMonsterType_.emtHeogho].CreateMonster();
                    break;
            }

            if(myMonsters[(int)_type][i].GetComponent<IDieSubject>() != null)
            {
                dieManager.AddMyFunc(myMonsters[(int)_type][i].GetComponent<IDieSubject>());
            }
            myMonsters[(int)_type][i].SetActive(false);
        }
    }

    public void SpawnMonsters(_ESpawnStageType_ _type)
    {
        for (int i = 0; i < myMonsters[(int)_type].Length; i++)
        {
            myMonsters[(int)_type][i].SetActive(true);
            myMonsters[(int)_type][i].transform.position = spawnPos[(int)_type][i].transform.position;
        }
    }

    public void SpawnMonsters()
    {
        for (int i = 0; i < (int)_ESpawnStageType_.esstMax; i++)
        {
            for (int j = 0; j < myMonsters[i].Length; j++)
            {
                myMonsters[i][j].SetActive(true);
                myMonsters[i][j].transform.position = spawnPos[i][j].transform.position;
            }  
        }
    }

    public void ClearMonsterList()
    {
        for (int i = 0; i < (int)_ESpawnStageType_.esstMax; i++)
        {
            for (int j = 0; j < myMonsters[i].Length; j++)
                myMonsters[i].SetValue(null, j);
        }
    }

    public void ClearMonsterList(_ESpawnStageType_ _type)
    {
        for (int i = 0; i < myMonsters[(int)_type].Length; i++)
            myMonsters[(int)_type].SetValue(null, i);
    }

    public GameObject[][] GetMonsters()
    {
        return myMonsters;
    }
}
