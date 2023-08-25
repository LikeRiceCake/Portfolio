using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    bool isDone;

    public void SpriteRun()
    {
        isDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }

    public void SpriteStop()
    {
        isDone = true;
    }

    public void SpriteOn()
    {
        m_Image.gameObject.SetActive(true);
    }
    
    public void SpriteOff()
    {
        m_Image.gameObject.SetActive(false);
    }

    IEnumerator Func_PlayAnimUI()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_Speed);
            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
            }
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;
            if (isDone)
                break;
        }
    }
}
