using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicStructs.Encyclopedia;
using PublicEnums.Monster;
using UnityEngine.SceneManagement;

public class Encyclopedia : MonoBehaviour
{
    Dictionary<string, CollectionInfo> monsterEncyclopedia = new Dictionary<string, CollectionInfo>();

    ResourceManager resourceManager;

    List<GameObject> collectionSpace = new List<GameObject>();

    [SerializeField] 
    GameObject[] detailInfoUI;

    void Start()
    {
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        CreateSpace((int)_EMonsterType_.eMax);
    }

    void Update()
    {
        OnOffEncyclopedia();
    }

    void OnOffEncyclopedia()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void CreateSpace(int _max)
    {
        for (int i = 0; i < _max; i++)
        {
            collectionSpace.Add(Instantiate(resourceManager.LoadCollectionSpace("Prefabs/Collection_Space"), gameObject.transform.GetChild(0)));
            collectionSpace[i].name = "Collection_Image_" + (i + 1).ToString("00");
            int index = i;
            collectionSpace[i].GetComponent<Button>().onClick.AddListener(() => SelectDetailInfo(index));
        }

        SetEncyclopedia(_max);
    }

    void SetEncyclopedia(int _max)
    {
        Image[] collectionImage = new Image[_max];

        for (int i = 0; i < _max; i++)
        {
            collectionImage[i] = gameObject.transform.GetChild(0).GetChild(i).GetComponent<Image>();

            if (collectionImage[i].sprite == null)
            {
                collectionImage[i].gameObject.SetActive(false);
            }
            else
            {
                collectionImage[i].gameObject.SetActive(true);
            }
        }
    }

    public void Collect(CollectionInfo _data)
    {
        if (true/*isClear*/)
        {
            if (monsterEncyclopedia.ContainsKey(_data.name))
            {
                return;
            }
            else
            {
                monsterEncyclopedia.Add(_data.name, _data);

                WriteInfo(_data.name);
                SetEncyclopedia((int)_EMonsterType_.eMax);
            }
        }
    }

    public void WriteInfo(string _name)
    {
        collectionSpace[monsterEncyclopedia[_name].numbering - 1].GetComponent<Image>().sprite = monsterEncyclopedia[_name].sprite;
        collectionSpace[monsterEncyclopedia[_name].numbering - 1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = monsterEncyclopedia[_name].name;
    }

    public void SelectDetailInfo(int _num)
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);

        string key = "";

        foreach (var number in monsterEncyclopedia.Values)
        {
            if (number.numbering - 1 == _num)
            {
                key = number.name;
                break;
            }
        }

        WriteDetailInfo(key);   
    }

    void WriteDetailInfo(string _key)
    {
        detailInfoUI[0].GetComponent<Text>().text = monsterEncyclopedia[_key].name;
        detailInfoUI[1].GetComponent<Image>().sprite = monsterEncyclopedia[_key].sprite;
        detailInfoUI[2].GetComponent<Text>().text = "No." + monsterEncyclopedia[_key].numbering;
        detailInfoUI[3].GetComponent<Text>().text = monsterEncyclopedia[_key].description;
        detailInfoUI[4].GetComponent<Text>().text = monsterEncyclopedia[_key].skillDescription;
    }

    public void CloseDetailInfo()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}