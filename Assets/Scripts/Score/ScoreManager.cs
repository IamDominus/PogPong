using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private BallController _ballController;
    public int LeftPlayerScore { get; private set; }
    public int RightPlayerScore { get; private set; }
    public event Action<int, bool> ScoreUpdated = delegate (int newScore, bool isLeftPlayerScored) { };
    private void Awake()
    {
        _ballController.PlayerScored += OnPlayerScored;
    }

    private void OnPlayerScored(bool isLeftPlayerScoreed)
    {
        if (isLeftPlayerScoreed)
        {
            LeftPlayerScore++;
            ScoreUpdated(LeftPlayerScore, isLeftPlayerScoreed);
        }
        else
        {
            RightPlayerScore++;
            ScoreUpdated(RightPlayerScore, isLeftPlayerScoreed);
        }
    }

    private void OnDestroy()
    {
        _ballController.PlayerScored -= OnPlayerScored;
    }
}
