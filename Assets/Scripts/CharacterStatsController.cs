using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    private PlayerMovement player;

    private int waterBallCount = 0;

    private Witcher witcher;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();

        Messenger<float>.AddListener("DamagePlayer", Damage) ;

        Messenger<float,float>.AddListener("SlowPlayer", SlowDown);

        Messenger<Witcher>.AddListener(Witcher.WitcherStates.WInitialize, InitializeWitcher);
    }

    private void InitializeWitcher(Witcher witcher)
    {
        this.witcher = witcher;
    }


    private void Damage(float damage)
    {
        player.Damage(damage);
    }

    private void SlowDown(float slowDownForce , float time)
    {
        if(witcher.witcherState != (Witcher.WitcherStates)0)
        {
            StartCoroutine(SlowDownSpeed(slowDownForce/2, time));
        }
        else
            StartCoroutine(SlowDownSpeed(slowDownForce, time));
    }

    IEnumerator SlowDownSpeed(float slowDownForce , float time)
    {
        player.Speed /= slowDownForce;

        yield return new WaitForSeconds(time);

        player.Speed *= slowDownForce;

        yield return null;
    }
    

}
