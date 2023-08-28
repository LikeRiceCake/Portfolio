using PublicEnums.UI.HP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHPUI : MonoBehaviour
{
    Image playerHp;
    Image bossHp;
    GameObject bossHpUI;

    void Start()
    {
        playerHp = transform.Find("PlayerHP").transform.Find("Image_Front").GetComponent<Image>();
        bossHpUI = transform.Find("BossHP").gameObject;
        bossHp = bossHpUI.transform.Find("Image_Front").GetComponent<Image>();
    }

    public void ChangeCharacterHPUI(_EHPUIType_ _type, int _currentHp, int _maxHp)
    {
        switch (_type)
        {
            case _EHPUIType_.ehutPlayer:
                playerHp.fillAmount = _currentHp / (float)_maxHp;
                break;
            case _EHPUIType_.ehutBoss:
                bossHp.fillAmount = _currentHp / (float)_maxHp;
                break;
        }
    }

    public void OnOffBossUI()
    {
        if (bossHpUI.gameObject.activeSelf)
            bossHpUI.gameObject.SetActive(false);
        else
            bossHpUI.gameObject.SetActive(true);
    }
    public void OffBossUI()
    {
        bossHpUI.gameObject.SetActive(false);
    }
}
