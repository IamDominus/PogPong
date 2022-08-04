using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private UnfreezeTimeCountdownManager _countdownManager;
    [SerializeField] private GameObject _rightPlayer;
    private void Awake()
    {
        Time.timeScale = 0f;

        _scoreManager.ScoreUpdated += OnScoreUpdated;

        if (Settings.PlayWithBot && _rightPlayer.TryGetComponent(out BotPlatformController botPlatformController))
        {
            botPlatformController.enabled = true;
        }
        else if (!Settings.PlayWithBot && _rightPlayer.TryGetComponent(out PlayerPlatformController playerPlatformController))
        {
            playerPlatformController.enabled = true;
        }

        _countdownManager.StartCountdown();
    }
    private void OnScoreUpdated(int newScore, bool isLeftPlayerScored)
    {
        if (_scoreManager.LeftPlayerScore >= Settings.FirstTo || _scoreManager.RightPlayerScore >= Settings.FirstTo)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    private void OnDestroy()
    {
        _scoreManager.ScoreUpdated -= OnScoreUpdated;
    }
}
