using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBattleArea : MonoBehaviour
{
    private BoxCollider2D column;

    [SerializeField] private float activateAfterSec;

    private Witcher witcher;

    private void Awake()
    {
        Messenger<Witcher>.AddListener(Witcher.WitcherStates.WInitialize, InitializeWitcher);
    }

    private void Start()
    {
        column = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Has Entered!");

        var player = other.GetComponent<PlayerMovement>();
        
        if (player != null)
        {
            StartCoroutine(ActivateWitcher(player.transform));

            column.isTrigger = false;

            player.InitializeEnemy(witcher.gameObject);
        }
    }

    private void InitializeWitcher(Witcher witcher)
    {
        this.witcher = witcher;
    }

    IEnumerator ActivateWitcher(Transform player)
    {
        yield return new WaitForSeconds(activateAfterSec);


        witcher.Activate(player);
    }

}
