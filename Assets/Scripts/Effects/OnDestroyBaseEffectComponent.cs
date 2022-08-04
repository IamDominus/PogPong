using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnDestroyBaseEffectComponent : MonoBehaviour
{
    protected abstract float EffectDuration { get; }
    public abstract void ApplyOnDestroyEffect(BallController ballController, PlatformController leftPlayer, PlatformController rightPlayer);
}
