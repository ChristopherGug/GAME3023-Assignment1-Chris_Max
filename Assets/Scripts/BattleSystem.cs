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
    public BattleUI playerUI,
                    enemyUI;

    public Ability ability;

    private bool win,
                 loss;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(player, playerSpawn);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemy, enemySpawn);
        enemyUnit = enemyGo.GetComponent<Unit>();

        battleText.text = "A wild " + enemyUnit.unitName + " has entered the battle!";

        playerUI.SetHUD(playerUnit);
        enemyUI.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        PlayerTurn();
    }

    IEnumerator PlayerMove()
    {
        enemyUI.SetHP(enemyUnit.currentHP);
       
        if (enemyUnit.currentHP <= 0)
        {
            win = true;
        }

        yield return new WaitForSeconds(2f);

        if (win)
        {
            currentState = BattleState.WIN;
            EndBattle();
        }
        else
        {
            currentState = BattleState.ENEMYTURN;
            //StartCoroutine(EnemyTurn());
        }
    }

    public void PlayerTurn()
    {
        battleText.text = "Your turn. Choose an action";
    }

    // For button usage
    public void OnAttackButton()
    {

        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        playerUnit.ability.Attack("Attack", 5, playerUnit, enemyUnit);

        battleText.text = "You used: " + playerUnit.ability.abilityName;

        StartCoroutine(PlayerMove());
        Debug.Log("OnAttackButton Worked");
    }

    public void EndBattle()
    {

    }
}
