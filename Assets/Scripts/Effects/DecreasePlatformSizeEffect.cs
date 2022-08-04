using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasePlatformSizeEffect : OnDestroyBaseEffectComponent
{
    [SerializeField] private float _sizeMultiplier;
    protected override float EffectDuration => 10f;

    public override void ApplyOnDestroyEffect(BallController ballController, PlatformController leftPlayer, PlatformController rightPlayer)
    {
        if (ballController.LeftPlayerLastTouchedTheBall)
        {
            rightPlayer.ChangeSize(_sizeMultiplier, EffectDuration);
        }
        else
        {
            leftPlayer.ChangeSize(_sizeMultiplier, EffectDuration);
        }
    }
}
