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
    public int manaCost;
    public int chanceOfSuccess;

    public Type type;

    public Unit unitCasting;
    public Unit unitReceiving;


    public void Attack(string _name, int _damage, Unit _unitCasting, Unit _unitReceiving)
    {
        abilityName = _name;
        _unitReceiving.TakeDamage(5);
    }

    public void Cast(string _name, Effect _effect, Type _type, Spell _spell, Unit _unitCasting, Unit _unitReceiving)
    {
        abilityName = _name;
        switch (_unitReceiving.currectEffect)
        {
            case Effect.NONE:
                if (_spell == Spell.FIRE)
                {
                    _unitReceiving.TakeDamage(10);
                    _unitReceiving.SetEffect(_effect);
                    _unitCasting.CostMP(5);
                }
                else if (_spell == Spell.WATER)
                {
                    _unitReceiving.TakeDamage(5);
                    _unitReceiving.SetEffect(_effect);
                    _unitCasting.CostMP(5);
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    _unitReceiving.TakeDamage(10);
                    _unitCasting.CostMP(5);
                }
                break;

            case Effect.WET:
                if (_spell == Spell.FIRE)
                {
                    _unitReceiving.TakeDamage(5);
                    _unitReceiving.SetEffect(Effect.NONE);
                    _unitCasting.CostMP(5);

                }
                else if (_spell == Spell.WATER)
                {
                    _unitReceiving.TakeDamage(5);
                    _unitCasting.CostMP(5);
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    _unitReceiving.TakeDamage(30);
                    _unitCasting.CostMP(5);
                }
                break;

            case Effect.BURN:
                if (_spell == Spell.FIRE)
                {
                    _unitReceiving.TakeDamage(10);
                    _unitCasting.CostMP(5);
                }
                else if (_spell == Spell.WATER)
                {
                    _unitReceiving.TakeDamage(5);
                    _unitReceiving.SetEffect(Effect.NONE);
                    _unitCasting.CostMP(5);
                }
                else if (_spell == Spell.LIGHTNING)
                {
                    _unitReceiving.TakeDamage(10);
                    _unitCasting.CostMP(5);
                }
                break;
        }
    }
}