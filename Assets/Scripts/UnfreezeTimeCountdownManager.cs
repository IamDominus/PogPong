using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnfreezeTimeCountdownManager : MonoBehaviour
{
    [SerializeField] private Text _countdownText;

    public void StartCountdown()
    {
        StartCoroutine(StartCountdownCoroutine());
    }
    private IEnumerator StartCountdownCoroutine()
    {
        int number = Constants.COUNTDOWN_SECONDS;
        _countdownText.text = number.ToString();
        _countdownText.enabled = true;

        while (number > 0)
        {
            _countdownText.text = number--.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        _countdownText.enabled = false;
        Time.timeScale = 1f;
    }
}
