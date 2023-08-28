using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    ResourceManager resourceManager;

    AudioMixer myMixer;

    AudioSource bgmSource;
    AudioSource sfxSource;

    AudioClip[] bgmClips;

    Slider[] soundSliders;

    const float MAX_VOLUME = 0f;
    const float MIN_VOLUME = -35f;
    const float MUTE_VOLUME = -80f;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();

        myMixer = resourceManager.LoadAudioMixer("Sounds/MyMixer");

        bgmSource = GameObject.Find("Audios").transform.Find("BGM").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("Audios").transform.Find("SFX").GetComponent<AudioSource>();

        bgmClips = new AudioClip[(int)_EMapType_.emtMax];

        bgmClips[(int)_EMapType_.emtTitle] = resourceManager.LoadAudioClip("Sounds/BGM/Title");
        bgmClips[(int)_EMapType_.emtIntro] = resourceManager.LoadAudioClip("Sounds/BGM/Intro");
        bgmClips[(int)_EMapType_.emtInGame] = resourceManager.LoadAudioClip("Sounds/BGM/InGame");
        bgmClips[(int)_EMapType_.emtGumihoFight] = resourceManager.LoadAudioClip("Sounds/BGM/GumihoFight");
        bgmClips[(int)_EMapType_.emtHeoghoFight] = resourceManager.LoadAudioClip("Sounds/BGM/HeoghoFight");

        soundSliders = new Slider[(int)_ESoundType_.estMax];
    }

    public void GetSliders()
    {
        soundSliders[(int)_ESoundType_.estMaster] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("MasterSlider").GetComponent<Slider>();
        soundSliders[(int)_ESoundType_.estBGM] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("BGMSlider").GetComponent<Slider>();
        soundSliders[(int)_ESoundType_.estSFX] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("SFXSlider").GetComponent<Slider>();

        soundSliders[(int)_ESoundType_.estMaster].maxValue = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.estBGM].maxValue = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.estSFX].maxValue = MAX_VOLUME;

        soundSliders[(int)_ESoundType_.estMaster].minValue = MIN_VOLUME;
        soundSliders[(int)_ESoundType_.estBGM].minValue = MIN_VOLUME;
        soundSliders[(int)_ESoundType_.estSFX].minValue = MIN_VOLUME;

        soundSliders[(int)_ESoundType_.estMaster].value = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.estBGM].value = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.estSFX].value = MAX_VOLUME;

        soundSliders[(int)_ESoundType_.estMaster].onValueChanged.AddListener(ControlMasterVolume);
        soundSliders[(int)_ESoundType_.estBGM].onValueChanged.AddListener(ControlBGMVolume);
        soundSliders[(int)_ESoundType_.estSFX].onValueChanged.AddListener(ControlSFXVolume);

        ControlMasterVolume(soundSliders[(int)_ESoundType_.estMaster].value);
        ControlBGMVolume(soundSliders[(int)_ESoundType_.estBGM].value);
        ControlSFXVolume(soundSliders[(int)_ESoundType_.estSFX].value);
        
    }
    public void PlayBGM(_EMapType_ _type)
    {
        bgmSource.clip = bgmClips[(int)_type];

        bgmSource.Play();
    }

    public void PlaySFX(AudioClip _clip)
    {
        sfxSource.PlayOneShot(_clip);
    }

    public void ControlMasterVolume(float _value)
    {
        if (_value <= MIN_VOLUME)
            _value = MUTE_VOLUME;
        myMixer.SetFloat("Master_Volume", _value);
    }

    public void ControlBGMVolume(float _value)
    {
        if (_value <= MIN_VOLUME)
            _value = MUTE_VOLUME;
        myMixer.SetFloat("BGM_Volume", _value);
    }

    public void ControlSFXVolume(float _value)
    {
        if (_value <= MIN_VOLUME)
            _value = MUTE_VOLUME;
        myMixer.SetFloat("SFX_Volume", _value);
    }
}
