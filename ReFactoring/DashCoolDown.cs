using PublicEnums;
using PublicEnums.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCoolDown : MonoBehaviour
{
    Image dashCoolImage;

    const float DASH_DELAY = 3f;

    public float m_currentDashCool { get; set; }

    void Start()
    {
        dashCoolImage = GameObject.Find("Dash_Image").transform.Find("Cool_Image").GetComponent<Image>();

        dashCoolImage.gameObject.SetActive(true);

        m_currentDashCool = DASH_DELAY;
    }

    void Update()
    {
        if (m_currentDashCool >= 0)
        {
            m_currentDashCool -= Time.deltaTime;
            dashCoolImage.fillAmount = m_currentDashCool / DASH_DELAY;
            if (m_currentDashCool <= 0)
            {
                dashCoolImage.gameObject.SetActive(false);
                Destroy(GetComponent<DashCoolDown>());
            }
        }
    }
}
