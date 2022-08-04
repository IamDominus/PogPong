using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public event Action<EffectController> EffectDestroyed = delegate (EffectController effectController) { };
    private void Start()
    {
        StartCoroutine(FadeOutAndDestroyCoroutine(Constants.DESTROY_EFFECT_TIME));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out BallController ball))
        {
            return;
        }

        StopAllCoroutines();

        if (TryGetComponent(out OnDestroyBaseEffectComponent onDestroyEffect)
            && ball.LeftPlayer.TryGetComponent(out PlatformController leftPlayerPlatformController)
            && ball.RightPlayer.TryGetComponent(out PlatformController rightPlayerPlatformController))
        {
            onDestroyEffect.ApplyOnDestroyEffect(ball, leftPlayerPlatformController, rightPlayerPlatformController);
        }

        EffectDestroyed(this);
        StartCoroutine(FadeOutAndDestroyCoroutine(0.3f));
    }
    private IEnumerator FadeOutAndDestroyCoroutine(float time)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        Color fadedToColor = GetComponent<SpriteRenderer>().color;
        fadedToColor.a = 0f;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = Color.Lerp(color, fadedToColor, t);
            GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
