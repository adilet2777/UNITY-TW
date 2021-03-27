using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Balls : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            BallAttack();
        }
        else if (collision.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

    protected abstract void BallAttack();
}
