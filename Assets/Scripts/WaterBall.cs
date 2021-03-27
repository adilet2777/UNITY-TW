using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Balls
{
    [SerializeField] private float slowForce;

    [SerializeField] private float time;
    protected override void BallAttack()
    {
        Messenger<float,float>.Broadcast("SlowPlayer", slowForce , time);
    }
}
