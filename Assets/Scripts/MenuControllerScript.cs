using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject SplashMenu;
    public Text AnyKeyText;
    public Text Title;
    public Text SecondTitle;
    float waitTime = 0.9f;

    bool big = true;

    void Start()
    {
        InvokeRepeating("BigOrSmall", waitTime, waitTime);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            MainMenu.gameObject.SetActive(true);
            SplashMenu.gameObject.SetActive(false);
            AnyKeyText.gameObject.SetActive(false);
        }
    }

    void BigOrSmall()
    {
        if (big == true)
        {
            Title.fontSize = 110;
            SecondTitle.fontSize = 60;
            big = false;
        }else
        {
            SecondTitle.fontSize = 70;
            Title.fontSize = 120;
            big = true;
        }
    }
}
