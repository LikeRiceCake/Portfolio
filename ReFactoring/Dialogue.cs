using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using PublicEnums;
using System;
using PublicEnums.State;

public class Dialogue : MonoBehaviour, IInputKeyClickObserver, IGameStateSubject
{
    enum _ELine_
    {
        eName,
        eLine,
        eMax
    }

    List<IGameStateObserver> myGameStateObs = new List<IGameStateObserver>();

    ResourceManager resourceManager;

    DialogueEvent dialogueEvent;

    RectTransform[] bgTrans;

    GameObject dialogueFrame;

    Sprite[] characterIllust;

    Image characterImage;

    Text nameText;
    Text lineText;

    _EDialogueEventType_ currentEventType;

    Vector3[] bgStartPos;

    Vector3[] bgGoalPos;

    string[,] myLine;

    int currentLineNumber;

    const float BG_SPEED = 5f;

    Coroutine currentBGRoutine;

    void Awake()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        dialogueFrame = GameObject.Find("Dialogue").transform.Find("Dialogue_Frame").gameObject;

        nameText = dialogueFrame.transform.Find("Name_Text").GetComponent<Text>();
        lineText = dialogueFrame.transform.Find("Line_Text").GetComponent<Text>();
        characterImage = dialogueFrame.transform.Find("Character_Image").GetComponent<Image>();

        bgTrans = new RectTransform[2];

        bgTrans[0] = GameObject.Find("Dialogue_BG_Upper").GetComponent<RectTransform>();
        bgTrans[1] = GameObject.Find("Dialogue_BG_Lower").GetComponent<RectTransform>();

        bgStartPos = new Vector3[2];

        bgStartPos[0] = new Vector3(0f, 1000f, 0f);
        bgStartPos[1] = new Vector3(0f, -440f, 0f);

        bgGoalPos = new Vector3[2];

        bgGoalPos[0] = new Vector3(0f, 775f, 0f);
        bgGoalPos[1] = new Vector3(0f, -225f, 0f);

        characterIllust = new Sprite[(int)_ECharacterImageType_.eMax];

        characterIllust[(int)_ECharacterImageType_.God] = resourceManager.LoadCharacterSprite("Sprite/Character/GodIllustSprite");
        characterIllust[(int)_ECharacterImageType_.Zero] = resourceManager.LoadCharacterSprite("Sprite/Character/ZeroIllustSprite");
        characterIllust[(int)_ECharacterImageType_.Tam] = resourceManager.LoadCharacterSprite("Sprite/Character/TamIllustSprite");
        characterIllust[(int)_ECharacterImageType_.Gumiho] = resourceManager.LoadCharacterSprite("Sprite/Character/GumihoIllustSprite");
        characterIllust[(int)_ECharacterImageType_.Heogho] = resourceManager.LoadCharacterSprite("Sprite/Character/HeoghoIllustSprite");
    }

    private void Start()
    {
        GameObject.Find("Player").GetComponent<IInputKeyClickSubject>().AddObserver(this);

        dialogueEvent = GetComponent<DialogueEvent>();
    }

    public void OnLine(_EDialogueEventType_ type)
    {
        if (currentBGRoutine != null)
            StopCoroutine(currentBGRoutine);

        currentBGRoutine = StartCoroutine(OnBG());
        InitDialogue(type);
    }

    void InitDialogue(_EDialogueEventType_ type)
    {
        myLine = resourceManager.LoadCummuLine("Text/" + type.ToString() + "Lines");

        currentLineNumber = 0;

        currentEventType = type;

        ProceedLine();
    }

    bool NextLineIdx()
    {
        currentLineNumber++;

        if (currentLineNumber >= myLine.GetLength(0))
            return false;

        return true;
    }

    void ProceedLine()
    {
        switch (myLine[currentLineNumber, (int)_ELine_.eName])
        {
            case "Ω≈":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.God];
                break;
            case "¡¶∑Œ":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.Zero];
                break;
            case "≈Ω":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.Tam];
                break;
            case "±∏πÃ»£":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.Gumiho];
                break;
            case "»Ê»£":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.Heogho];
                break;
            case "???":
                characterImage.sprite = characterIllust[(int)_ECharacterImageType_.Gumiho];
                break;
        }

        nameText.text = myLine[currentLineNumber, (int)_ELine_.eName];
        lineText.text = myLine[currentLineNumber, (int)_ELine_.eLine];
    }

    IEnumerator OnBG()
    {
        NotifyGameState(_EGameStateType_.eisStop, true);

        while (true)
        {
            if (bgTrans[0].localPosition == bgGoalPos[0] && bgTrans[1].localPosition == bgGoalPos[1])
                break;

            bgTrans[0].localPosition = Vector3.MoveTowards(bgTrans[0].localPosition, bgGoalPos[0], BG_SPEED);

            bgTrans[1].localPosition = Vector3.MoveTowards(bgTrans[1].localPosition, bgGoalPos[1], BG_SPEED);

            yield return null;
        }

        dialogueFrame.SetActive(true);

        NotifyGameState(_EGameStateType_.eisLine, true);
    }

    IEnumerator OffBG()
    {
        NotifyGameState(_EGameStateType_.eisLine, false);

        dialogueFrame.SetActive(false);

        while (true)
        {
            if (bgTrans[0].localPosition == bgStartPos[0] && bgTrans[1].localPosition == bgStartPos[1])
                break;

            bgTrans[0].localPosition = Vector3.MoveTowards(bgTrans[0].localPosition, bgStartPos[0], BG_SPEED);

            bgTrans[1].localPosition = Vector3.MoveTowards(bgTrans[1].localPosition, bgStartPos[1], BG_SPEED);

            yield return null;
        }

        NotifyGameState(_EGameStateType_.eisStop, false);

        dialogueEvent.DoEvent(currentEventType);
    }


    public void ReactNotify(_EInputType_ type, _EInputDetailType_ dType)
    {
        if (type == _EInputType_.eDialogue)
        {
            switch (dType)
            {
                case _EInputDetailType_.eDialogue:
                    if(!NextLineIdx())
                    {
                        if (currentBGRoutine != null)
                            StopCoroutine(currentBGRoutine);

                        currentBGRoutine = StartCoroutine(OffBG());
                    }
                    else
                        ProceedLine();
                    break;
            }
        }
    }

    public void NotifyGameState(_EGameStateType_ type, bool state)
    {
        foreach (var ob in myGameStateObs)
            ob.ReactNotify(type, state);
    }

    public void AddObserver(IGameStateObserver ob)
    {
        myGameStateObs.Add(ob);
    }

    public void RemoveObserver(IGameStateObserver ob)
    {
        myGameStateObs.Remove(ob);
    }
}
