using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int damage;

    public int maxHP;
    public int currentHP;

    public Effect currectEffect;

    public Ability ability;

    public void TakeDamage(int _damage)
    {
        currentHP -= _damage;
    }
}
