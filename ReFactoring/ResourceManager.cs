using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    Dictionary<string, GameObject> collectionSpace = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> monsterPrefab = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> skillPrefab = new Dictionary<string, GameObject>();
    Dictionary<string, Sprite> monsterSprite = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> skillSprite = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> characterSprite = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> comicSprite = new Dictionary<string, Sprite>();
    Dictionary<string, Mesh> tamMesh = new Dictionary<string, Mesh>();
    Dictionary<string, AudioClip> audioClip = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioMixer> audioMixer = new Dictionary<string, AudioMixer>();
    Dictionary<string, string[,]> cummuLine = new Dictionary<string, string[,]>();
    Dictionary<string, string[]> loadingLine = new Dictionary<string, string[]>();

    public GameObject LoadCollectionSpace(string _path)
    {
        if(collectionSpace.ContainsKey(_path))
            return collectionSpace[_path];
        else
        {
            collectionSpace.Add(_path, Resources.Load<GameObject>(_path));
            return collectionSpace[_path];
        }
    }

    public GameObject LoadMonsterPrefab(string _path)
    {
        if (monsterPrefab.ContainsKey(_path))
            return monsterPrefab[_path];
        else
        {
            monsterPrefab.Add(_path, Resources.Load<GameObject>(_path));
            return monsterPrefab[_path];
        }
    }
    
    public GameObject LoadSkillPrefab(string _path)
    {
        if (skillPrefab.ContainsKey(_path))
            return skillPrefab[_path];
        else
        {
            skillPrefab.Add(_path, Resources.Load<GameObject>(_path));
            return skillPrefab[_path];
        }
    }

    public Sprite LoadMonsterSprite(string _path)
    {
        if (monsterSprite.ContainsKey(_path))
            return monsterSprite[_path];
        else
        {
            monsterSprite.Add(_path, Resources.Load<Sprite>(_path));
            return monsterSprite[_path];
        }
    }

    public Sprite LoadSkillSprite(string _path)
    {
        if (skillSprite.ContainsKey(_path))
            return skillSprite[_path];
        else
        {
            skillSprite.Add(_path, Resources.Load<Sprite>(_path));
            return skillSprite[_path];
        }
    }

    public Mesh LoadTamMesh(string _path)
    {
        if (tamMesh.ContainsKey(_path))
            return tamMesh[_path];
        else
        {
            tamMesh.Add(_path, Resources.Load<GameObject>(_path).GetComponent<MeshFilter>().sharedMesh);
            return tamMesh[_path];
        }
    }
    
    public AudioClip LoadAudioClip(string _path)
    {
        if (audioClip.ContainsKey(_path))
            return audioClip[_path];
        else
        {
            audioClip.Add(_path, Resources.Load<AudioClip>(_path));
            return audioClip[_path];
        }
    }
    
    public AudioMixer LoadAudioMixer(string _path)
    {
        if (audioMixer.ContainsKey(_path))
            return audioMixer[_path];
        else
        {
            audioMixer.Add(_path, Resources.Load<AudioMixer>(_path));
            return audioMixer[_path];
        }
    }
    
    public Sprite LoadCharacterSprite(string _path)
    {
        if (characterSprite.ContainsKey(_path))
            return characterSprite[_path];
        else
        {
            characterSprite.Add(_path, Resources.Load<Sprite>(_path));
            return characterSprite[_path];
        }
    }
    
    public Sprite LoadComicSprite(string _path)
    {
        if (comicSprite.ContainsKey(_path))
            return comicSprite[_path];
        else
        {
            comicSprite.Add(_path, Resources.Load<Sprite>(_path));
            return comicSprite[_path];
        }
    }
    
    public string[,] LoadCummuLine(string _path)
    {
        if (cummuLine.ContainsKey(_path))
            return cummuLine[_path];
        else
        {
            TextAsset temp = Resources.Load<TextAsset>(_path);
            string[] line = temp.text.Split('\n');
            int lineSize = line.Length;
            int rowSize = line[0].Split('\t').Length;
            string[,] sentence = new string[lineSize, rowSize];

            for(int i = 0; i < lineSize; i++)
            {
                string[] row = line[i].Split('\t');
                for(int j = 0; j < rowSize; j++)
                {
                    sentence[i, j] = row[j];
                }
            }

            cummuLine.Add(_path, sentence);

            return cummuLine[_path];
        }
    }
    
    public string[] LoadLoadingLine(string _path)
    {
        if (loadingLine.ContainsKey(_path))
            return loadingLine[_path];
        else
        {
            TextAsset temp = Resources.Load<TextAsset>(_path);
            string[] line = temp.text.Split('\n');

            loadingLine.Add(_path, line);

            return loadingLine[_path];
        }
    }
}
