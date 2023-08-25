using PublicEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFunc : MonoBehaviour
{
    enum _EIntroComicType_
    {
        eFirst,
        eSecond,
        eThird,
        eMax
    }

    ResourceManager resourceManager;

    MapLoader mapLoader;

    Image introComicImage;

    Sprite[] introComicSprites;

    bool isIn;
    bool isOut;

    float alpha;

    int currentComic;

    private void Start()
    {
        mapLoader = GameObject.Find("Manager").GetComponent<MapLoader>();

        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();

        introComicImage = GameObject.Find("IntroCanvas").transform.Find("Canvas").transform.Find("BG").transform.Find("Wallpaper_Image").GetComponent<Image>();

        introComicSprites = new Sprite[(int)_EIntroComicType_.eMax];

        introComicSprites[(int)_EIntroComicType_.eFirst] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_1");
        introComicSprites[(int)_EIntroComicType_.eSecond] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_2");
        introComicSprites[(int)_EIntroComicType_.eThird] = resourceManager.LoadComicSprite("Sprite/Intro/IntroSprite_3");

        currentComic = 0;

        alpha = 1f;

        introComicImage.sprite = introComicSprites[currentComic];
        introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, alpha);
    }

    IEnumerator FadeIn()
    {
        while (true)
        {
            alpha += Time.deltaTime;
            introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, alpha);

            if (alpha >= 1f)
                break;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            alpha -= Time.deltaTime;
            introComicImage.color = new Color(introComicImage.color.r, introComicImage.color.g, introComicImage.color.b, alpha);

            if (alpha <= 0f)
            {
                if (currentComic < (int)_EIntroComicType_.eThird)
                {
                    currentComic++;
                    introComicImage.sprite = introComicSprites[currentComic];
                    StartCoroutine(FadeIn());
                    break;
                }
                else if (currentComic == (int)_EIntroComicType_.eThird)
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
