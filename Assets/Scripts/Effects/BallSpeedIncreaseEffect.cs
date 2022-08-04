using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedIncreaseEffect : OnDestroyBaseEffectComponent
{
    [SerializeField] private float _speedMultiplier;
    protected override float EffectDuration => 5f;

    public override void ApplyOnDestroyEffect(BallController ballController, PlatformController leftPlayer, PlatformController rightPlayer)
    {
        ballController.ChangeSpeed(_speedMultiplier, 5f);
    }
}
