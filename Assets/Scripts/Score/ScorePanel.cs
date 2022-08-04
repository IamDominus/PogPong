using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Text _leftPlayerScoreText;
    [SerializeField] private Text _rightPlayerScoreText;
    private void Awake()
    {
        _scoreManager.ScoreUpdated += OnScoreUpdated;
    }
    private void OnScoreUpdated(int newScore, bool isLeftPlayerScored)
    {
        if (isLeftPlayerScored)
        {
            _leftPlayerScoreText.text = newScore.ToString();
            StartCoroutine(BlinkText(_leftPlayerScoreText));
        }
        else
        {
            _rightPlayerScoreText.text = newScore.ToString();
            StartCoroutine(BlinkText(_rightPlayerScoreText));
        }
    }
    private IEnumerator BlinkText(Text text)
    {
        bool flag = true;
        Color color = text.color;
        float defaultAlpha = text.color.a;

        for (float i = text.color.a + 0.01f; i > defaultAlpha;)
        {
            if (i >= 1f)
            {
                flag = false;
            }
            if (flag)
            {
                i += 0.01f;
            }
            else
            {
                i -= 0.01f;
            }

            color.a = i;
            text.color = color;
            yield return new WaitForSeconds(0.003f);
        }
    }

    private void OnDestroy()
    {
        _scoreManager.ScoreUpdated -= OnScoreUpdated;
    }
}
