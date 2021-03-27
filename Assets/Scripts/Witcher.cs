using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Witcher : MonoBehaviour , IDamagable
{
    private BattleMovement battleMove;

    [SerializeField] private float health;

    public WitcherStates witcherState;

    private int characterAttackCount = 0 , witcherAttackCount = 0 ;

    [SerializeField] private FireBall fireBallConfig;

    [SerializeField] private WaterBall waterBallConfig;

    [SerializeField] private float ballLiveTime;

    private Transform player;

    [SerializeField] private Slider hpSlider;

    [SerializeField] private int superAttackBallsCount;

    private void Awake()
    {
        Messenger<BattleMovement,WitcherStates>.AddListener(WitcherStates.WChangeBattleStyle, ChangeBattleStyle);      
    }

    private void Start()
    {
        witcherState = WitcherStates.Witcher_STATE1;

        Messenger<float>.Broadcast(WitcherStates.WHealthChanged, health);

        Messenger<Witcher>.Broadcast(WitcherStates.WInitialize, this);
    }

    public enum WitcherStates
    {
        Witcher_STATE1,
        Witcher_STATE2,
        Witcher_STATE3,
        WChangeBattleStyle,
        WInitialize,
        WHealthChanged
    }

    public void Activate(Transform player)
    {
        this.player = player;
        Attack();
    }


    private void ChangeBattleStyle(BattleMovement battleMovement , WitcherStates state)
    {
        witcherState = state;

        battleMove = battleMovement;
    }

    public void Damage(float damage)
    {
        health -= damage;

        characterAttackCount++;

        Debug.Log(characterAttackCount+ "      attack couint");

        hpSlider.value = health;

        InvokeChecker();

        Messenger<float>.Broadcast(WitcherStates.WHealthChanged, health);
    }

    private void InvokeChecker()
    {
        Messenger<int, int>.Broadcast(witcherState, characterAttackCount, witcherAttackCount);
    }

    public void Attack()
    {
        StartCoroutine(AttackByTime());
    }

    IEnumerator AttackByTime()
    {
        while (true)
        {

            witcherAttackCount++;

            var ball = ChooseRandom();

            InstantiateBalls(ball , player.position);

            InvokeChecker();

            yield return new WaitForSeconds(1/(battleMove.AttackSpeed/10));
        }
    }
    private void InstantiateBalls(Balls ball , Vector3 pointToDeliver)
    {
        var shootBall = Instantiate(ball);

        shootBall.transform.position = transform.position;

        shootBall.GetComponent<Rigidbody2D>().velocity = (pointToDeliver - shootBall.transform.position).normalized * 10f;
    }

    private Balls ChooseRandom()
    {
        var random = Random.Range(0, 100);
        if (random %2 == 0)
        {
            return fireBallConfig;
        }
        else
            return waterBallConfig;
    }

    public void Move(in int attackCountMinus , in int wAttackCountMinus)
    {
        characterAttackCount -= attackCountMinus;
        witcherAttackCount -= wAttackCountMinus;

        transform.position = new Vector2(Random.Range(20, 40), Random.Range(-1,1));
    }

}
