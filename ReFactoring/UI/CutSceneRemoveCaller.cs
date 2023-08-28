using PublicEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneRemoveCaller : MonoBehaviour
{
    MapLoader mapLoader;

    MonsterSpawner monsterSpawner;

    Dialogue dialogue;

    private void OnEnable()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();

        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();

        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        if (SceneManager.GetActiveScene().name == _EMapType_.emtMiddleBossTransformation.ToString())
            monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstMiddleBossRoom_A);
        else if(SceneManager.GetActiveScene().name == _EMapType_.emtFinalBossAppear.ToString())
        {
            monsterSpawner.SpawnMonsters(_ESpawnStageType_.esstFinalBossRoom);
            dialogue.OnLine(_EDialogueEventType_.edetEndInstantiateFinalBoss);
        }

        mapLoader.StartUnLoadMap();
    }
}