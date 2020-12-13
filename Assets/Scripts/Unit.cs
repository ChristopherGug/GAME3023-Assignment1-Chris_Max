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
    public AudioSource audioSource;
    public AudioClip[] audioClips;

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
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
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

    public void PlaySound(int _clip, float _startTime, float _volume)
    {
        audioSource.clip = audioClips[_clip];
        audioSource.time = _startTime;
        audioSource.volume = _volume;
        audioSource.Play();
    }
}
