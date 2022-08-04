using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] protected bool _isLeftPlayer;
    [SerializeField] protected ScoreManager _scoreManager;
    protected float _speed = Constants.DEFAULT_PLAYER_SPEED;
    protected Vector3 _defaultSacale;
    protected Vector3 _defaultPosition;
    protected virtual void Awake()
    {
        _scoreManager.ScoreUpdated += OnScoreUpdated;
        _defaultSacale = transform.localScale;
        _defaultPosition = transform.position;
    }
    private void OnScoreUpdated(int newScore, bool isLeftPlayerScored)
    {

        StopAllCoroutines();
        transform.localScale = _defaultSacale;
        transform.position = _defaultPosition;
    }
    private void OnDestroy()
    {
        _scoreManager.ScoreUpdated -= OnScoreUpdated;
    }
    public void ChangeSize(float sizeMultiplier, float duration)
    {
        StartCoroutine(ChangeSizeCoroutine(sizeMultiplier, duration));
    }
    private IEnumerator ChangeSizeCoroutine(float sizeMultiplier, float duration)
    {
        var scale = transform.localScale;
        scale.y *= sizeMultiplier;
        transform.localScale = scale;
        yield return new WaitForSeconds(duration);
        scale = transform.localScale;
        scale.y /= sizeMultiplier;
        transform.localScale = scale;
    }
    protected void MoveUp()
    {
        transform.position += Vector3.up * _speed;
    }
    protected void MoveDown()
    {
        transform.position += Vector3.down * _speed;
    }
}
