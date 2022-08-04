using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallDirectionEffect : OnDestroyBaseEffectComponent
{
    protected override float EffectDuration => 5f;

    public override void ApplyOnDestroyEffect(BallController ballController, PlatformController leftPlayer, PlatformController rightPlayer)
    {
        ballController.RandomizeDirection();
    }
}
