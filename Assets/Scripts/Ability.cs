using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    NONE,
    WET,
    BURN
}

public enum Type
{
    ATTACK,
    SPELL,
    FLEE,
    STRUGGLE
}

public enum Spell
{
    FIRE,
    WATER,
    LIGHTNING
}

public class Ability : MonoBehaviour
{
    public string abilityName;

    public int damage;
    public int chanceOfSuccess;

    public Type type;

    public Unit unitCasting;
    public Unit unitReceiving;

    public void Attack(string _name, int _damage, Unit _unitCasting, Unit _unitReceiving)
    {
        name = _name;
        unitReceiving.TakeDamage(5);
    }

    public void Cast(string _name, Effect _effect, Type _type, Spell _spell, Unit _unitCasting, Unit _unitReceiving)
    {
        name = _name;
        switch (_effect)
        {
            case Effect.NONE:
                if (_spell == Spell.FIRE)
                {
                    unitReceiving.TakeDamage(10);
                    unitReceiving.currectEffect = Effect.BURN;
                }
                else if (_spell == Spell.WATER)
                {
                    unitReceiving.TakeDamage(5);
                    unitReceiving.currectEffect = Effect.WET;
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    unitReceiving.TakeDamage(10);
                }
                break;

            case Effect.WET:
                if (_spell == Spell.FIRE)
                {
                    unitReceiving.TakeDamage(5);
                    unitReceiving.currectEffect = Effect.NONE;
                }
                else if (_spell == Spell.WATER)
                {
                    unitReceiving.TakeDamage(5);
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    unitReceiving.TakeDamage(30);
                }
                break;

            case Effect.BURN:
                if (_spell == Spell.FIRE)
                {
                    unitReceiving.TakeDamage(10);
                }
                else if (_spell == Spell.WATER)
                {
                    unitReceiving.TakeDamage(5);
                    unitReceiving.currectEffect = Effect.NONE;
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    unitReceiving.TakeDamage(10);
                }
                break;
        }
    }
}