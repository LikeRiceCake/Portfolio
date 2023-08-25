using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicEnums;

public abstract class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    protected float COOLTIME;

    [SerializeField]
    protected int skillValue;

    public float currentCoolTime { get; set; }

    protected Sprite mySprite;

    protected Image skillUIImage;

    protected ResourceManager resourceManager;

    protected Image coolImage;

    public virtual void ChangeImage()
    {
        skillUIImage.sprite = mySprite;
        coolImage.sprite = mySprite;
        if(currentCoolTime <= 0)
            coolImage.gameObject.SetActive(false);
        else
            coolImage.gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        if(currentCoolTime > 0f)
        {
            currentCoolTime -= Time.deltaTime;
            if (currentCoolTime <= 0)
                coolImage.gameObject.SetActive(false);
        }
    }

    public virtual void SkillFunction()
    {
        coolImage.gameObject.SetActive(true);
    }

    protected virtual void Awake()
    {
        skillUIImage = GameObject.Find("Skill_Image").GetComponent<Image>();
        coolImage = skillUIImage.transform.Find("Cool_Image").GetComponent<Image>();
        resourceManager = GameObject.Find("Manager").GetComponent<ResourceManager>();
    }
}
