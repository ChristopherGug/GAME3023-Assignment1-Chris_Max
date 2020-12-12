using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public Slider healthSlider;
    public Slider manaSlider;

    public void SetHUD(Unit _unit)
    {
        nameText.text = _unit.unitName;
        healthText.SetText((_unit.currentHP.ToString() + "/" + _unit.maxHP.ToString()));
        manaText.SetText((_unit.currentMP.ToString() + "/" + _unit.maxMP.ToString()));

        healthSlider.maxValue = _unit.maxHP;
        healthSlider.value = _unit.currentHP;

        manaSlider.maxValue = _unit.maxMP;
        manaSlider.value = _unit.currentMP;
    }

    public void SetHP(int _hp, Unit _unit)
    {
        healthSlider.value = _hp;
        healthText.SetText((_unit.currentHP.ToString() + "/" + _unit.maxHP.ToString()));
    }
    public void SetMP(int _mp, Unit _unit)
    {
        manaSlider.value = _mp;
        manaText.SetText((_mp.ToString() + "/" + _unit.maxMP.ToString()));

    }


}
