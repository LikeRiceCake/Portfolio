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

        bgmClips = new AudioClip[(int)_EMapType_.eMax];

        bgmClips[(int)_EMapType_.eTitle] = resourceManager.LoadAudioClip("Sounds/BGM/Title");
        bgmClips[(int)_EMapType_.eIntro] = resourceManager.LoadAudioClip("Sounds/BGM/Intro");
        bgmClips[(int)_EMapType_.eInGame] = resourceManager.LoadAudioClip("Sounds/BGM/InGame");
        bgmClips[(int)_EMapType_.eGumihoFight] = resourceManager.LoadAudioClip("Sounds/BGM/GumihoFight");
        bgmClips[(int)_EMapType_.eHeoghoFight] = resourceManager.LoadAudioClip("Sounds/BGM/HeoghoFight");

        soundSliders = new Slider[(int)_ESoundType_.eMax];
    }

    public void GetSliders()
    {
        soundSliders[(int)_ESoundType_.eMaster] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("MasterSlider").GetComponent<Slider>();
        soundSliders[(int)_ESoundType_.eBGM] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("BGMSlider").GetComponent<Slider>();
        soundSliders[(int)_ESoundType_.eSFX] = GameObject.Find("ManageCanvas").transform.Find("Canvas").transform.Find("OptionWallpaper_Image").transform.Find("Scroll_Image").transform.Find("Settings").transform.Find("SFXSlider").GetComponent<Slider>();

        soundSliders[(int)_ESoundType_.eMaster].maxValue = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.eBGM].maxValue = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.eSFX].maxValue = MAX_VOLUME;

        soundSliders[(int)_ESoundType_.eMaster].minValue = MIN_VOLUME;
        soundSliders[(int)_ESoundType_.eBGM].minValue = MIN_VOLUME;
        soundSliders[(int)_ESoundType_.eSFX].minValue = MIN_VOLUME;

        soundSliders[(int)_ESoundType_.eMaster].value = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.eBGM].value = MAX_VOLUME;
        soundSliders[(int)_ESoundType_.eSFX].value = MAX_VOLUME;

        soundSliders[(int)_ESoundType_.eMaster].onValueChanged.AddListener(ControlMasterVolume);
        soundSliders[(int)_ESoundType_.eBGM].onValueChanged.AddListener(ControlBGMVolume);
        soundSliders[(int)_ESoundType_.eSFX].onValueChanged.AddListener(ControlSFXVolume);

        ControlMasterVolume(soundSliders[(int)_ESoundType_.eMaster].value);
        ControlBGMVolume(soundSliders[(int)_ESoundType_.eBGM].value);
        ControlSFXVolume(soundSliders[(int)_ESoundType_.eSFX].value);
        
    }
    public void PlayBGM(_EMapType_ type)
    {
        bgmSource.clip = bgmClips[(int)type];

        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ControlMasterVolume(float value)
    {
        if (value <= MIN_VOLUME)
            value = MUTE_VOLUME;
        myMixer.SetFloat("Master_Volume", value);
    }

    public void ControlBGMVolume(float value)
    {
        if (value <= MIN_VOLUME)
            value = MUTE_VOLUME;
        myMixer.SetFloat("BGM_Volume", value);
    }

    public void ControlSFXVolume(float value)
    {
        if (value <= MIN_VOLUME)
            value = MUTE_VOLUME;
        myMixer.SetFloat("SFX_Volume", value);
    }
}
