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

    public float currentDashCool { get; set; }

    void Start()
    {
        dashCoolImage = GameObject.Find("Dash_Image").transform.Find("Cool_Image").GetComponent<Image>();

        dashCoolImage.gameObject.SetActive(true);

        currentDashCool = DASH_DELAY;
    }

    void Update()
    {
        if (currentDashCool >= 0)
        {
            currentDashCool -= Time.deltaTime;
            dashCoolImage.fillAmount = currentDashCool / DASH_DELAY;
            if (currentDashCool <= 0)
            {
                dashCoolImage.gameObject.SetActive(false);
                Destroy(GetComponent<DashCoolDown>());
            }
        }
    }
}
