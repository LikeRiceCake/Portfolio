using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFunc : MonoBehaviour
{
    enum _EIntroComicType_
    {
        eictFirst,
        eictSecond,
        eictThird,
        eictMax
    }

    ResourceManager resourceManager;

    MapLoader mapLoader;

    Image introComicImage;

    Sprite[] introComicSprites;

    float m_alpha;

    int m_currentComic;

    private void Start()
    {
        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();

        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        introComicImage = GameObject.Find("IntroCanvas").transform.Find("Canvas").transform.Find("BG").transform.Find("Wallpaper_Image").GetComponent<Image>();

        introComicSprites = new Sprite[(int)_EIntroComicType_.eictMax];

        introComicSprites[(int)_EIntroComicType_.eictFirst] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_1");
        introComicSprites[(int)_EIntroComicType_.eictSecond] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_2");
        introComicSprites[(int)_EIntroComicType_.eictThird] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_3");

        m_currentComic = 0;

        m_alpha = 1f;

        introComicImage.sprite = introComicSprites[m_currentComic];
        introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, m_alpha);
    }

    IEnumerator FadeIn()
    {
        while (true)
        {
            m_alpha += Time.deltaTime;
            introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, m_alpha);

            if (m_alpha >= 1f)
                break;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            m_alpha -= Time.deltaTime;
            introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, m_alpha);

            if (m_alpha <= 0f)
            {
                if (m_currentComic < (int)_EIntroComicType_.eictThird)
                {
                    m_currentComic++;
                    introComicImage.sprite = introComicSprites[m_currentComic];
                    StartCoroutine(FadeIn());
                    break;
                }
                else if (m_currentComic == (int)_EIntroComicType_.eictThird)
                {
                    mapLoader.StartUnLoadMap(_EMapType_.eIntro);
                    mapLoader.StartUnLoadMap(_EMapType_.eTitle);
                    mapLoader.StartLoadMap(_EMapType_.eInGame);
                    break;
                }
            }

            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (introComicImage.color.a >= 1f)
                StartCoroutine(FadeOut());
        }
    }
}
