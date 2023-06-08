using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation
{
    public void PlayIdleAnimation();

    public void PlayBattleIdleAnimation();

    public void PlayRunAnimation();

    public void PlayAttackAnimation();

    public void PlayOnHitAnimation();

    public void PlayDeathAnimation();
}