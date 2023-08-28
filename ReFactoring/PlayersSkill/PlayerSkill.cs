using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicEnums;

public abstract class PlayerSkill : MonoBehaviour
{
    public float m_currentCoolTime { get; set; }

    protected Sprite mySprite;

    protected Image skillImage;

    protected ResourceManager resourceManager;

    protected Image coolImage;

    public virtual void ChangeImage()
    {
        skillImage.sprite = mySprite;
        coolImage.sprite = mySprite;
        if(m_currentCoolTime <= 0)
            coolImage.gameObject.SetActive(false);
        else
            coolImage.gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        if(m_currentCoolTime > 0f)
        {
            m_currentCoolTime -= Time.deltaTime;
            if (m_currentCoolTime <= 0)
                coolImage.gameObject.SetActive(false);
        }
    }

    public virtual void ActivateSkillFunction()
    {
        coolImage.gameObject.SetActive(true);
    }

    protected virtual void Awake()
    {
        skillImage = GameObject.Find("Skill_Image").GetComponent<Image>();
        coolImage = skillImage.transform.Find("Cool_Image").GetComponent<Image>();
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
    }
}
