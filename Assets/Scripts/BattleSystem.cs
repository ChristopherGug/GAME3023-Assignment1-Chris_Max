﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public float chanceToFlee,
               chanceToStruggle;

    public Animator spellAnimation;

    // Start is called before the first frame update
    void Start()
    {
        ability = new Ability();
        currentState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void LateUpdate()
    {
       
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
        Debug.Log((playerUnit.animator.GetInteger("TypeOfAttack")).ToString());

        if (playerUnit.animator.GetInteger("TypeOfAttack") == 1)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(playerUnit.animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(spellAnimation.GetCurrentAnimatorStateInfo(0).length);
        }

        enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);
        playerUI.SetMP(playerUnit.currentMP, playerUnit);

        playerUnit.animator.SetInteger("TypeOfAttack", 0);
        spellAnimation.SetInteger("Spell", 0);

        if (enemyUnit.currentHP <= 0)
        {
            win = true;
        }

        yield return new WaitForSeconds(2f);

        if (win)
        {
            currentState = BattleState.WIN;
            StartCoroutine(EndBattle());
        }
        else
        {
            currentState = BattleState.ENEMYTURN;

            StopAllCoroutines();

            StartCoroutine(EnemyTurn());
        }
    }

    public void PlayerTurn()
    {
        currentState = BattleState.PLAYERTURN;
        battleText.text = "Your turn. Choose an action";

        if (playerUnit.currentHP <= 0)
        {
            currentState = BattleState.LOSE;
            StopAllCoroutines();
            StartCoroutine(EndBattle());
        }

        if (playerUnit.currectEffect == Effect.BURN)
        {
            playerUnit.TakeDamage(5);
            playerUI.SetHP(playerUnit.currentHP, playerUnit);
        }
    }

    // For button usage
    public void OnAttackButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        ability.Attack("Attack", 5, playerUnit, enemyUnit);

        battleText.text = "You used: " + ability.abilityName;

        playerUnit.animator.SetInteger("TypeOfAttack", 1);

        playerUnit.PlaySound(0, 0.5f, 0.5f);

        StopAllCoroutines();

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

            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);
            playerUI.SetMP(playerUnit.currentMP, playerUnit);

            playerUnit.animator.SetInteger("TypeOfAttack", 2);
            spellAnimation.SetInteger("Spell", 1);

            playerUnit.PlaySound(1, 0.5f, 0.5f);

            StopAllCoroutines();

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
            if (currentState != BattleState.PLAYERTURN)
            {
                return;
            }

            ability.Cast("Water", Effect.WET, Type.SPELL, Spell.WATER, playerUnit, enemyUnit);

            battleText.text = "You used: " + ability.abilityName;

            playerUnit.animator.SetInteger("TypeOfAttack", 3);
            spellAnimation.SetInteger("Spell", 3);

            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);
            playerUI.SetMP(playerUnit.currentMP, playerUnit);

            playerUnit.PlaySound(2, 0.5f, 0.5f);

            StopAllCoroutines();

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
            if (currentState != BattleState.PLAYERTURN)
            {
                return;
            }

            ability.Cast("Lightning", Effect.NONE, Type.SPELL, Spell.LIGHTNING, playerUnit, enemyUnit);

            battleText.text = "You used: " + ability.abilityName;

            playerUnit.animator.SetInteger("TypeOfAttack", 4);
            spellAnimation.SetInteger("Spell", 5);

            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);
            playerUI.SetMP(playerUnit.currentMP, playerUnit);

            playerUnit.PlaySound(3, 0.5f, 0.5f);

            StopAllCoroutines();

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
        chanceToStruggle = ((float)enemyUnit.currentHP / (float)enemyUnit.maxHP) * 100.0f;
        int random = Random.Range(0, 101);

        battleText.text = "You used Struggle...";

        yield return new WaitForSeconds(1f);

        Debug.Log("Chance to Struggle: " + chanceToStruggle.ToString() +
                  "\nRoll: " + random.ToString());

        if (random > chanceToStruggle)
        {
            enemyUnit.TakeDamage(enemyUnit.currentHP);
            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);

            battleText.text = "Struggle was successful!";
            currentState = BattleState.WIN;

            yield return new WaitForSeconds(2f);
            StartCoroutine(EndBattle());
        }

        else if (random <= chanceToStruggle)
        {
            battleText.text = "Struggle failed!";
            currentState = BattleState.ENEMYTURN;

            yield return new WaitForSeconds(2f);

            StopAllCoroutines();
            StartCoroutine(EnemyTurn());
        }

    }

    public void OnStruggleButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        StopAllCoroutines();

        StartCoroutine(Struggle());
    }

    IEnumerator Flee()
    {
        chanceToFlee = ((float)playerUnit.currentHP / (float)playerUnit.maxHP) * 100;

        int random = Random.Range(0, 101);

        battleText.text = "You try to flee...";

        yield return new WaitForSeconds(1f);

        Debug.Log("Chance to Flee: " + chanceToFlee.ToString() +
                  "\nRoll: " + random.ToString());

        if (random > chanceToFlee)
        {
            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);

            battleText.text = "You flee successfully!";
            currentState = BattleState.ESCAPE;

            yield return new WaitForSeconds(2f);
            StartCoroutine(EndBattle());
        }

        else if (random <= chanceToFlee)
        {
            battleText.text = "You fail to flee!";
            currentState = BattleState.ENEMYTURN;

            yield return new WaitForSeconds(2f);
            StopAllCoroutines();

            StartCoroutine(EnemyTurn());
        }
    }
    public void OnFleeButton()
    {
        if (currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        StopAllCoroutines();

        StartCoroutine(Flee());
    }

    IEnumerator EnemyTurn()
    {
        battleText.text = enemyUnit.unitName + "'s turn!";

        if (enemyUnit.currectEffect == Effect.BURN)
        {
            enemyUnit.TakeDamage(5);
            enemyUI.SetHP(enemyUnit.currentHP, enemyUnit);

            if(enemyUnit.currentHP <= 0)
            {
                currentState = BattleState.WIN;
                StopAllCoroutines();
                StartCoroutine(EndBattle());
            }
        }

        yield return new WaitForSeconds(1f);

        if (enemyUnit.currentMP > 0)
        {
            if (playerUnit.currectEffect == Effect.NONE)
            {
                enemyAbilityUse = Random.Range(0, 8);
                if (enemyAbilityUse <= 4)
                {
                    ability.Attack("Attack", 3, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 1);

                    enemyUnit.PlaySound(0, 0, 0.5f);
                }
                else if (enemyAbilityUse == 5)
                {
                    ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 2);
                    spellAnimation.SetInteger("Spell", 2);
                    enemyUnit.PlaySound(1, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse == 6)
                {
                    ability.Cast("Water", Effect.WET, Type.SPELL, Spell.WATER, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 3);
                    spellAnimation.SetInteger("Spell", 4);
                    enemyUnit.PlaySound(2, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse == 7)
                {
                    ability.Cast("Lightning", Effect.NONE, Type.SPELL, Spell.LIGHTNING, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 4);
                    spellAnimation.SetInteger("Spell", 6);
                    enemyUnit.PlaySound(3, 0.5f, 0.5f);
                }
                battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

                playerUI.SetHP(playerUnit.currentHP, playerUnit);
                enemyUI.SetMP(enemyUnit.currentMP, enemyUnit);
            }
            else if (playerUnit.currectEffect == Effect.BURN)
            {
                enemyAbilityUse = Random.Range(0, 11);
                if (enemyAbilityUse <= 5)
                {
                    ability.Attack("Attack", 3, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 1);
                    enemyUnit.PlaySound(0, 0, 0.5f);
                }
                else if (enemyAbilityUse > 5 && enemyAbilityUse <= 7)
                {
                    ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 2);
                    spellAnimation.SetInteger("Spell", 2);
                    enemyUnit.PlaySound(1, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse > 7 && enemyAbilityUse <= 9)
                {
                    ability.Cast("Lightning", Effect.NONE, Type.SPELL, Spell.LIGHTNING, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 4);
                    spellAnimation.SetInteger("Spell", 6);
                    enemyUnit.PlaySound(3, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse == 10)
                {
                    ability.Cast("Water", Effect.WET, Type.SPELL, Spell.WATER, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 3);
                    spellAnimation.SetInteger("Spell", 4);
                    enemyUnit.PlaySound(2, 0.5f, 0.5f);
                }
                battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

                playerUI.SetHP(playerUnit.currentHP, playerUnit);
                enemyUI.SetMP(enemyUnit.currentMP, enemyUnit);
            }
            else if (playerUnit.currectEffect == Effect.WET)
            {
                enemyAbilityUse = Random.Range(0, 13);
                if (enemyAbilityUse <= 4)
                {
                    ability.Attack("Attack", 3, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 1);
                    enemyUnit.PlaySound(0, 0, 0.5f);
                }
                else if (enemyAbilityUse > 5 && enemyAbilityUse <= 7)
                {
                    ability.Cast("Water", Effect.WET, Type.SPELL, Spell.WATER, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 3);
                    spellAnimation.SetInteger("Spell", 4);
                    enemyUnit.PlaySound(2, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse > 7 && enemyAbilityUse <= 11)
                {
                    ability.Cast("Lightning", Effect.NONE, Type.SPELL, Spell.LIGHTNING, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 4);
                    spellAnimation.SetInteger("Spell", 6);
                    enemyUnit.PlaySound(3, 0.5f, 0.5f);
                }
                else if (enemyAbilityUse == 12)
                {
                    ability.Cast("Fire", Effect.BURN, Type.SPELL, Spell.FIRE, enemyUnit, playerUnit);
                    enemyUnit.animator.SetInteger("TypeOfAbility", 2);
                    spellAnimation.SetInteger("Spell", 2);
                    enemyUnit.PlaySound(1, 0.5f, 0.5f);
                }
                battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

                playerUI.SetHP(playerUnit.currentHP, playerUnit);
                enemyUI.SetMP(enemyUnit.currentMP, enemyUnit);

            }
        }
        else if (enemyUnit.currentMP <= 0)
        {
            ability.Attack("Attack", 3, enemyUnit, playerUnit);
            battleText.text = enemyUnit.unitName + " used " + ability.abilityName;

            playerUI.SetHP(playerUnit.currentHP, playerUnit);
            enemyUI.SetMP(enemyUnit.currentMP, enemyUnit);

            enemyUnit.animator.SetInteger("TypeOfAbility", 1);
        }
        Debug.Log("Enemy Attack Roll: " + enemyAbilityUse.ToString());



        if (enemyUnit.animator.GetInteger("TypeOfAttack") == 1)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(enemyUnit.animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(spellAnimation.GetCurrentAnimatorStateInfo(0).length);
        }

        spellAnimation.SetInteger("Spell", 0);
        enemyUnit.animator.SetInteger("TypeOfAbility", 0);

        if (playerUnit.currentHP <= 0)
        {
            loss = true;
        }

        if (loss)
        {
            currentState = BattleState.LOSE;

            StartCoroutine(EndBattle());
        }
        else
        {
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
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

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Overworld");
    }
}
