using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonMenuScript : MonoBehaviour {

    public GameObject MainMenuButton1;
    public GameObject MainMenuButton2;
    public GameObject DiffucultyMenu;

    public void ClickedGame()
    {
        MainMenuButton1.gameObject.SetActive(false);
        MainMenuButton2.gameObject.SetActive(false);
        DiffucultyMenu.gameObject.SetActive(true);
    }

    public void ClickedExit()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public void ClickedBackToMain()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
