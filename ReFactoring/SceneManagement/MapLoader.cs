using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Resources;
using PublicEnums;
using PublicEnums.State;
using Unity.VisualScripting;

public class MapLoader : MonoBehaviour, IGameStateSubject
{
    List<IGameStateObserver> myObs = new List<IGameStateObserver>();

    SpriteAnimationRunner spriteRunner;

    CutSceneUI cutSceneUI;

    InitHelper initHelper;

    const float LOAD_FORCE_TIME = 2f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        spriteRunner = GameObject.Find("UI").GetComponent<SpriteAnimationRunner>();

        cutSceneUI = GameObject.Find("UI").GetComponent<CutSceneUI>();

        initHelper = GameObject.Find("InitHelper").GetComponent<InitHelper>();
    }

    private void Start()
    {
        StartLoadMap(_EMapType_.eTitle);
    }

    public void StartLoadMap(_EMapType_ _type)
    {
        spriteRunner.SetRunImage("Loading_Image");
        spriteRunner.OnImage();
        spriteRunner.RunImage();

        StartCoroutine(LoadMap(_type));
    }

    IEnumerator LoadMap(_EMapType_ _type)
    {
        switch (_type)
        {
            case _EMapType_.eMiddleBossTransformation:
            case _EMapType_.eFinalBossAppear:
                NotifyGameState(_EGameStateType_.egstIsStop, true);
                cutSceneUI.OffUIs();
                break;
        }

        yield return new WaitForSeconds(LOAD_FORCE_TIME);

        AsyncOperation LoadHelper = SceneManager.LoadSceneAsync(_type.ToString(), LoadSceneMode.Additive);

        while (!LoadHelper.isDone)
        {
            Debug.Log("¸Ê ·Îµå : " + LoadHelper.progress);

            if (LoadHelper.progress >= 0.9f)
            {
                spriteRunner.StopImage();
                spriteRunner.OffImage();
            }

            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_type.ToString()));

        switch (_type)
        {
            case _EMapType_.eInGame:
                initHelper.CallInit(_EInitCallType_.eictEnter_InGameScene);
                break;
        }
    }

    public void StartUnLoadMap(_EMapType_ type)
    {
        StartCoroutine(UnLoadMap(type));
    }

    IEnumerator UnLoadMap(_EMapType_ type)
    {
        AsyncOperation UnLoadHelper = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(type.ToString()));

        if (UnLoadHelper != null)
        {
            while (!UnLoadHelper.isDone)
            {
                Debug.Log("¸Ê ¾ð·Îµå : " + UnLoadHelper.progress);

                yield return null;
            }
        }
    }
    
    public void StartUnLoadMap()
    {
        StartCoroutine(UnLoadMap());
    }

    IEnumerator UnLoadMap()
    {
        AsyncOperation UnLoadHelper = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        if (UnLoadHelper != null)
        {
            while (!UnLoadHelper.isDone)
            {
                Debug.Log("¸Ê ¾ð·Îµå : " + UnLoadHelper.progress);

                yield return null;
            }
        }

        NotifyGameState(_EGameStateType_.egstIsStop, false);
        cutSceneUI.OnUIs();

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("eInGame"));
    }

    public void NotifyGameState(_EGameStateType_ type, bool state)
    {
        foreach (var ob in myObs)
            ob.ReactNotify(type, state);
    }

    public void AddObserver(IGameStateObserver ob)
    {
        myObs.Add(ob);
    }

    public void RemoveObserver(IGameStateObserver ob)
    {
        myObs.Remove(ob);
    }
}