using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int damage;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public Effect currectEffect;

    public SpriteRenderer statusSprite;
    public Sprite fire,
                  water;

    public Animator animator;

    public void SetEffect(Effect _effect)
    {
        currectEffect = _effect;
    }
    public void TakeDamage(int _damage)
    {
        currentHP -= _damage;
    }
    public void CostMP(int _cost)
    {
        currentMP -= _cost;
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        switch(currectEffect)
        {
            case Effect.NONE:
                statusSprite.enabled = false;
                break;

            case Effect.BURN:
                statusSprite.enabled = true;
                statusSprite.sprite = fire;
                break;

            case Effect.WET:
                statusSprite.enabled = true;
                statusSprite.sprite = water;
                break;
        }
    }
}
