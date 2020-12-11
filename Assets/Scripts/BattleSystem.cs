using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WIN,
    LOSE
}

public class BattleSystem : MonoBehaviour
{
    public BattleState currentState;

    public GameObject player,
                      enemy;
    public Transform playerSpawn,
                     enemySpawn;

    Unit playerUnit,
         enemyUnit;

    public TMP_Text battleText;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BattleState.START;

        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGo = Instantiate(player, playerSpawn);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemy, enemySpawn);
        enemyUnit = enemyGo.GetComponent<Unit>();

        battleText.text = "A wild " + enemyUnit.unitName + " has entered the battle!";
    }
}
