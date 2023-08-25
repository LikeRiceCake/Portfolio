using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PublicEnums.Monster;
using PublicEnums;

public class MonsterSpawner : MonoBehaviour
{
    MonsterFactory[] myFactory;

    GameObject[][] myMonsters;

    GameObject[][] spawnPos;

    DieManager dieManager;

    void Start()
    {
        dieManager = GameObject.Find("Manager").GetComponent<DieManager>();

        myFactory = new MonsterFactory[(int)_EMonsterType_.eMax];
        myMonsters = new GameObject[(int)_ESpawnStageType_.eMax][];
        spawnPos = new GameObject[(int)_ESpawnStageType_.eMax][];

        for (int i = 0; i < (int)_ESpawnStageType_.eMax; i++)
        {
            spawnPos[i] = new GameObject[GameObject.Find(((_ESpawnStageType_)i).ToString()).transform.childCount];
            for (int j = 0; j < spawnPos[i].Length; j++)
                spawnPos[i][j] = GameObject.Find(((_ESpawnStageType_)i).ToString()).transform.GetChild(j).gameObject;
        }

        for (int i = 0; i < (int)_ESpawnStageType_.eMax; i++)
            myMonsters[i] = new GameObject[spawnPos[i].Length];

        myFactory[(int)_EMonsterType_.eBoar] = gameObject.AddComponent<BoarFactory>();
        myFactory[(int)_EMonsterType_.eBird] = gameObject.AddComponent<BirdFactory>();
        myFactory[(int)_EMonsterType_.eWolf] = gameObject.AddComponent<WolfFactory>();
        myFactory[(int)_EMonsterType_.eGumihoH] = gameObject.AddComponent<GumihoHFactory>();
        myFactory[(int)_EMonsterType_.eGumihoA] = gameObject.AddComponent<GumihoAFactory>();
        myFactory[(int)_EMonsterType_.eGumihoIdle] = gameObject.AddComponent<GumihoIdleFactory>();
        myFactory[(int)_EMonsterType_.eHeogho] = gameObject.AddComponent<HeoghoFactory>();
    }

    public void CreateMonsters()
    {
        for (int i = 0; i < (int)_ESpawnStageType_.eMax; i++)
        {
            for (int j = 0; j < spawnPos[i].Length; j++)
            {
                switch (spawnPos[i][j].tag)
                {
                    case "BoarSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eBoar].CreateMonster();
                        break;
                    case "BirdSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eBird].CreateMonster();
                        break;
                    case "WolfSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eWolf].CreateMonster();
                        break;
                    case "GumihoHSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eGumihoH].CreateMonster();
                        break;
                    case "GumihoASpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eGumihoA].CreateMonster();
                        break;
                    case "GumihoIdleSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eGumihoIdle].CreateMonster();
                        break;
                    case "HeoghoSpawn":
                        myMonsters[i][j] = myFactory[(int)_EMonsterType_.eHeogho].CreateMonster();
                        break;
                }
                if (myMonsters[i][j].GetComponent<IDieSubject>() != null)
                    dieManager.AddMyFunc(myMonsters[i][j].GetComponent<IDieSubject>());
                myMonsters[i][j].SetActive(false);
            }
        }
    }

    public void CreateMonsters(_ESpawnStageType_ type)
    {
        for (int i = 0; i < spawnPos[(int)type].Length; i++)
        {
            switch (spawnPos[(int)type][i].tag)
            {
                case "BoarSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eBoar].CreateMonster();
                    break;
                case "BirdSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eBird].CreateMonster();
                    break;
                case "WolfSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eWolf].CreateMonster();
                    break;
                case "GumihoHSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eGumihoH].CreateMonster();
                    break;
                case "GumihoASpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eGumihoA].CreateMonster();
                    break;
                case "GumihoIdleSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eGumihoIdle].CreateMonster();
                    break;
                case "HeoghoSpawn":
                    myMonsters[(int)type][i] = myFactory[(int)_EMonsterType_.eHeogho].CreateMonster();
                    break;
            }

            if(myMonsters[(int)type][i].GetComponent<IDieSubject>() != null)
            {
                dieManager.AddMyFunc(myMonsters[(int)type][i].GetComponent<IDieSubject>());
            }
            myMonsters[(int)type][i].SetActive(false);
        }
    }

    public void SpawnMonsters(_ESpawnStageType_ type)
    {
        for (int i = 0; i < myMonsters[(int)type].Length; i++)
        {
            myMonsters[(int)type][i].SetActive(true);
            myMonsters[(int)type][i].transform.position = spawnPos[(int)type][i].transform.position;
        }
    }

    public void SpawnMonsters()
    {
        for (int i = 0; i < (int)_ESpawnStageType_.eMax; i++)
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
        for (int i = 0; i < (int)_ESpawnStageType_.eMax; i++)
        {
            for (int j = 0; j < myMonsters[i].Length; j++)
                myMonsters[i].SetValue(null, j);
        }
    }

    public void ClearMonsterList(_ESpawnStageType_ type)
    {
        for (int i = 0; i < myMonsters[(int)type].Length; i++)
            myMonsters[(int)type].SetValue(null, i);
    }

    public GameObject[][] GetMonsters()
    {
        return myMonsters;
    }
}
