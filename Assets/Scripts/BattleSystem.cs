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
    LOSE,
    ESCAPE
}

public class BattleSystem : MonoBehaviour
{
    public BattleState currentState;

    public GameObject player,
                      enemy1,
                      enemy2,
                      enemy3;
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

    private int enemyAbilityUse;

    public int chanceToFlee,
               chanceToStruggle;

    // Start is called before the first frame update
    void Start()
    {
        ability = new Ability();
        currentState = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    private void LateUpdate()
    {
       Debug.Log("HP: " + enemyUnit.currentHP);
    }

    IEnumerator SetupBattle()
    {
        win = false;
        loss = false;

        GameObject playerGo = Instantiate(player, playerSpawn);
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo;

        int randomNumber = Random.Range(1, 4);

        switch(randomNumber)
        {
            case 1:
                enemyGo = Instantiate(enemy1, enemySpawn);
                enemyUnit = enemyGo.GetComponent<Unit>();
                break;
            case 2:
                enemyGo = Instantiate(enemy2, enemySpawn);
                enemyUnit = enemyGo.GetComponent<Unit>();
                break;
            case 3:
                enemyGo = Instantiate(enemy3, enemySpawn);
                enemyUnit = enemyGo.GetComponent<Unit>();
                break;
        }

        playerUnit.currentMP = playerUnit.maxMP;
        enemyUnit.currentHP = enemyUnit.maxHP;
        enemyUnit.currentMP = enemyUnit.maxMP;

        battleText.text = "A wild " + enemyUnit.unitName + " has entered the battle!";

        playerUI.SetHUD(playerUnit);
        enemyUI.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        PlayerTurn();
    }

    IEnumerator PlayerMove()
    {
        enemyUI.SetHP(enemyUnit.currentHP);
        playerUI.SetMP(playerUnit.currentMP);

        yield return new WaitForSeconds(0.5f);

        playerUnit.animator.SetBool("Idle", true);
        playerUnit.animator.SetBool("Attacking", false);

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
            StartCoroutine(EnemyTurn());
        }
    }

    public void PlayerTurn()
    {
        currentState = BattleState.PLAYERTURN;
        battleText.text = "Your turn. Choose an action";
    }

    // For button usage
    public void OnAttackButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        ability.Attack("Attack", 5, playerUnit, enemyUnit);

        enemyUI.SetHP(enemyUnit.currentHP);

        battleText.text = "You used: " + ability.abilityName;

        playerUnit.animator.SetBool("Idle", false);
        playerUnit.animator.SetBool("Attacking", true);

        StartCoroutine(PlayerMove());

        //Debug.Log(ability.abilityName);
    }

    public void OnFireButton()
    {
        if (playerUnit.currentMP >= 5)
        {
            Debug.Log("OnFireButton Worked");

            if (currentState != BattleState.PLAYERTURN)
            {
                return;
            }

            ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, playerUnit, enemyUnit);

            battleText.text = "You used: " + ability.abilityName;

            enemyUI.SetHP(enemyUnit.currentHP);
            playerUI.SetMP(playerUnit.currentMP);

            StartCoroutine(PlayerMove());

            //Debug.Log(ability.abilityName);
        }
        else
        {
            battleText.text = "Not enough mana!";
        }
    }
    public void OnWaterButton()
    {
        if (playerUnit.currentMP >= 5)
        {
            Debug.Log("OnFireButton Worked");

            if (currentState != BattleState.PLAYERTURN)
            {
                return;
            }

            ability.Cast("Water", Effect.WET, Type.SPELL, Spell.WATER, playerUnit, enemyUnit);

            battleText.text = "You used: " + ability.abilityName;

            enemyUI.SetHP(enemyUnit.currentHP);
            playerUI.SetMP(playerUnit.currentMP);

            StartCoroutine(PlayerMove());

            //Debug.Log(enemyUnit.currentHP);
        }
        else
        {
            battleText.text = "Not enough mana!";
        }
    }
    public void OnLightningButton()
    {
        if (playerUnit.currentMP >= 5)
        {
            //Debug.Log("OnLightningButton Worked");

            if (currentState != BattleState.PLAYERTURN)
            {
                return;
            }

            ability.Cast("Lightning", Effect.NONE, Type.SPELL, Spell.LIGHTNING, playerUnit, enemyUnit);

            battleText.text = "You used: " + ability.abilityName;

            enemyUI.SetHP(enemyUnit.currentHP);
            playerUI.SetMP(playerUnit.currentMP);

            StartCoroutine(PlayerMove());

            //Debug.Log(enemyUnit.currentHP);
        }
        else
        {
            battleText.text = "Not enough mana!";
        }
    }

    IEnumerator Struggle()
    {
        chanceToStruggle = (3 / enemyUnit.currentHP) * 100;

        int random = Random.Range(0, 101);

        battleText.text = "You used Struggle...";

        yield return new WaitForSeconds(1f);

        if (random <= chanceToStruggle)
        {
            enemyUnit.TakeDamage(enemyUnit.currentHP);
            enemyUI.SetHP(enemyUnit.currentHP);

            battleText.text = "Struggle was successful!";
            currentState = BattleState.WIN;

            yield return new WaitForSeconds(2f);
            EndBattle();
        }

        else if (random > chanceToStruggle)
        {
            battleText.text = "Struggle failed!";
            currentState = BattleState.ENEMYTURN;

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnStruggleButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(Struggle());
    }

    IEnumerator Flee()
    {
        chanceToFlee = (3 / playerUnit.currentHP) * 100;

        int random = Random.Range(0, 101);

        battleText.text = "You try to flee...";

        yield return new WaitForSeconds(1f);

        if (random <= chanceToFlee)
        {
            enemyUnit.TakeDamage(enemyUnit.currentHP);
            enemyUI.SetHP(enemyUnit.currentHP);

            battleText.text = "You flee successfully!";
            currentState = BattleState.ESCAPE;

            yield return new WaitForSeconds(2f);
            EndBattle();
        }

        else if (random > chanceToStruggle)
        {
            battleText.text = "You fail to flee!";
            currentState = BattleState.ENEMYTURN;

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }
    public void OnFleeButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(Flee());
    }

    IEnumerator EnemyTurn()
    {
        battleText.text = enemyUnit.unitName + "'s turn!";

        yield return new WaitForSeconds(2f);

        if (playerUnit.currectEffect == Effect.NONE)
        {
            enemyAbilityUse = Random.Range(0, 8);
            if(enemyAbilityUse <= 5)
            {
                ability.Attack("Attack", 3, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse == 6)
            {
                ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse == 7)
            {
                ability.Cast("Water", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse == 8)
            {
                ability.Cast("Lightning", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

            playerUI.SetHP(playerUnit.currentHP);
            enemyUI.SetMP(enemyUnit.currentMP);
        }
        else if (playerUnit.currectEffect == Effect.BURN)
        {
            enemyAbilityUse = Random.Range(0, 10);
            if (enemyAbilityUse <= 5)
            {
                ability.Attack("Attack", 3, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse > 5 || enemyAbilityUse <= 7)
            {
                ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse > 7 || enemyAbilityUse <= 9)
            {
                ability.Cast("Lightning", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse == 10)
            {
                ability.Cast("Water", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

            playerUI.SetHP(playerUnit.currentHP);
            enemyUI.SetMP(enemyUnit.currentMP);
        }
        else if (playerUnit.currectEffect == Effect.WET)
        {
            enemyAbilityUse = Random.Range(0, 10);
            if (enemyAbilityUse <= 5)
            {
                ability.Attack("Attack", 3, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse > 5 || enemyAbilityUse <= 7)
            {
                ability.Cast("Water", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse > 7 || enemyAbilityUse <= 9)
            {
                ability.Cast("Lightning", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            else if (enemyAbilityUse == 10)
            {
                ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
            }
            battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

            playerUI.SetHP(playerUnit.currentHP);
            enemyUI.SetMP(enemyUnit.currentMP);
        }

        if (playerUnit.currentHP <= 0)
        {
            loss = true;
        }


        yield return new WaitForSeconds(2f);

        if (loss)
        {
            currentState = BattleState.WIN;
            EndBattle();
        }
        else
        {
            PlayerTurn();
        }
    }

    public void EndBattle()
    {
        if (currentState == BattleState.WIN)
        {
            battleText.text = "You defeated the " + enemyUnit.unitName;
        }
        else if (currentState == BattleState.LOSE)
        {
            battleText.text = "You have died...";
        }
        else if (currentState == BattleState.ESCAPE)
        {
            battleText.text = "You run away.";
        }
    }

    //IEnumerator ReceiveAbility()
    //{

    //}
}
