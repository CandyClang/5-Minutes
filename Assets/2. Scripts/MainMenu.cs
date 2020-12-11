using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("CutsceneIntro");
    }

    public void PlayGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MapSceneBackup");
    }


    public void ReturnToMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
