using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private BallController _ballController;
    [SerializeField] private Vector2 _topLeftSpawnPosition;
    [SerializeField] private Vector2 _bottomRightSpawnPosition;
    [SerializeField] private GameObject[] _effectPrefabs;
    private List<GameObject> _spawnedEffects = new List<GameObject>();
    private int _ballHitPlayerCount;

    private void Awake()
    {
        _scoreManager.ScoreUpdated += OnScoreUpdated;
        _ballController.BallHitPlayer += OnBallHitPlayer;
    }
    private void Start()
    {
    }
    private void OnScoreUpdated(int newScore, bool isLeftPlayerScored)
    {
        ResetEfects();
    }
    private void OnBallHitPlayer()
    {
        if (++_ballHitPlayerCount >= 3 && Random.value > 0.7f)
        {
            SpawnRandomEffect();
            _ballHitPlayerCount = 0;
        }
    }
    private void ResetEfects()
    {
        foreach (var spawnedEffect in _spawnedEffects)
        {
            if (spawnedEffect != null)
            {
                Destroy(spawnedEffect);
            }
        }
        _spawnedEffects.Clear();
    }
    public void SpawnRandomEffect()
    {
        if (_effectPrefabs.Length == 0)
        {
            return;
        }

        Vector2 spawnPosition = new Vector2(
            Random.Range(_topLeftSpawnPosition.x, _bottomRightSpawnPosition.x),
            Random.Range(_topLeftSpawnPosition.y, _bottomRightSpawnPosition.y));

        var effect = _effectPrefabs[Random.Range(0, _effectPrefabs.Length)];

        var spawnedEffect = Instantiate(effect, spawnPosition, Quaternion.identity);
        _spawnedEffects.Add(spawnedEffect);
    }
    private void OnDestroy()
    {
        _scoreManager.ScoreUpdated -= OnScoreUpdated;
        _ballController.BallHitPlayer -= OnBallHitPlayer;
    }
}
