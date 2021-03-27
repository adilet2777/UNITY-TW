using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Balls 
{
    [SerializeField] private float damage;
    protected override void BallAttack()
    {
        Messenger<float>.Broadcast("DamagePlayer", damage);
    }

}
