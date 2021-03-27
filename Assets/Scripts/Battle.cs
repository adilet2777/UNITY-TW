using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Battle : MonoBehaviour
{
    private BattleMovement battleStyle;

    private Witcher witcher;

    private void Awake()
    {
        Messenger<Witcher>.AddListener(Witcher.WitcherStates.WInitialize, InitializeWitcher);

        Messenger<BattleMovement,Witcher.WitcherStates>.AddListener(Witcher.WitcherStates.WChangeBattleStyle, ChangeBattleStyle);


        Messenger<int, int>.AddListener(Witcher.WitcherStates.Witcher_STATE1, FirstStyle);
        Messenger<int, int>.AddListener(Witcher.WitcherStates.Witcher_STATE2, SecondStyle);
        Messenger<int, int>.AddListener(Witcher.WitcherStates.Witcher_STATE3, ThirdStyle);

    }




    private void InitializeWitcher(Witcher witcher)
    {
        this.witcher = witcher;
    }

    private void ChangeBattleStyle(BattleMovement battleStyle , Witcher.WitcherStates state)
    {
        this.battleStyle = battleStyle;
    }

    private void FirstStyle( int attackCount ,  int wAttackCount)
    {
        Debug.Log("FIRST STYLE IS CURRENT STATE");
        MoveAfterPlayerAttack(attackCount);
        MoveAfterWitcherAttack(wAttackCount);
    }

    private void SecondStyle( int attackCount,  int wAttackCount)
    {
        Debug.Log("SECOND STYLE IS CURRENT STATE");
        MoveAfterPlayerAttack(attackCount);
    }
    private void ThirdStyle( int attackCount, int wAttackCount)
    {
        Debug.Log("THIRD STYLE IS CURRENT STATE");
        MoveAfterWitcherAttack(wAttackCount);

        if(wAttackCount == 3)
        {
        }
    }
    private void MoveAfterWitcherAttack(in int attackCount)
    {
        if(attackCount == battleStyle.WAttack)
        {
            witcher.Move(0, attackCount);
        }
    }

    private void MoveAfterPlayerAttack(in int attackCount)
    {
        if(attackCount == battleStyle.PlayerAttack)
        {
            witcher.Move(attackCount, 0);
        }
    }


}
