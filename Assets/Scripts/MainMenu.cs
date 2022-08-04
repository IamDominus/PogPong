using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int FirstTo
    {
        get
        {
            return Settings.FirstTo;
        }
        set
        {
            Settings.FirstTo = value;
        }
    }
    public bool PlayWithBot
    {
        get
        {
            return Settings.PlayWithBot;
        }
        set
        {
            Settings.PlayWithBot = value;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
