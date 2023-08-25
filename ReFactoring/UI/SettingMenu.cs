using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{

    public Dropdown DResolution;
    public Toggle FScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1440, 900, true);
                break;
            case 2:
                Screen.SetResolution(1280, 720, true);
                break;
        }
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void RoadScene()
    {
        SceneManager.LoadScene("1");
    }
}
