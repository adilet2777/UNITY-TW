using UnityEngine;
using System.Collections.Generic;
using System;

public class WHealthManager : MonoBehaviour
{
    private Witcher witcher;

    [SerializeField] private BattleMovement[] battleStyles = new BattleMovement[3];

    private bool firstTime;

    private float maxHealth;

    private void Awake()
    {
        Messenger<Witcher>.AddListener(Witcher.WitcherStates.WInitialize, InitializeWitcher);

        Messenger<float>.AddListener(Witcher.WitcherStates.WHealthChanged, CheckIfChangeStyle);
    }

    private void Start()
    {
        InvokeState(1);
    }

    private void InitializeWitcher(Witcher witcher)
    {
        this.witcher = witcher;
    }

    private void CheckIfChangeStyle(float health)
    {
        if (!firstTime)
        {
            firstTime = true;
            maxHealth = health;
        }

        var stateHP = maxHealth / 3;
        //first state
        if (health > stateHP * 2)
        {
            InvokeState(1);
        }
        //second state
        else if( health > stateHP && health <= stateHP * 2)
        {
            InvokeState(2);
        }
        //third state
        else
        {
            InvokeState(3);
        }

        Debug.Log("HEALTH IS : " + health);

    }

    private void InvokeState(in int state)
    {
        //send event that style changed                                                                                                  send which state / later to check 
        Messenger<BattleMovement, Witcher.WitcherStates>.Broadcast(Witcher.WitcherStates.WChangeBattleStyle, battleStyles[state - 1] , (Witcher.WitcherStates)state-1 );
    }
}

[Serializable]
public class BattleMovement
{
    [SerializeField] private int playerAttack;
    public int PlayerAttack => playerAttack;

    [SerializeField] private int wAttack;
    public int WAttack => wAttack;

    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed;
}