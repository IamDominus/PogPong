using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UnfreezeTimeCountdownManager _countdownManager;
    [SerializeField] private GameObject _pauseMenu;
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _countdownManager.StartCountdown();
    }
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
