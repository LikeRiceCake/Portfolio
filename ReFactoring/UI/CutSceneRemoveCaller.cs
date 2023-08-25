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

        if (SceneManager.GetActiveScene().name == _EMapType_.eMiddleBossTransformation.ToString())
            monsterSpawner.SpawnMonsters(_ESpawnStageType_.eMiddleBossRoom_A);
        else if(SceneManager.GetActiveScene().name == _EMapType_.eFinalBossAppear.ToString())
        {
            monsterSpawner.SpawnMonsters(_ESpawnStageType_.eFinalBossRoom);
            dialogue.OnLine(_EDialogueEventType_.eEndInstantiateFinalBoss);
        }

        mapLoader.StartUnLoadMap();
    }
}