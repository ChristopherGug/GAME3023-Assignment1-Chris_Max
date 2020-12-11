using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public TMP_Text nameText;
    public Slider healthSlider;

    public void SetHUD(Unit _unit)
    {
        nameText.text = _unit.unitName;
        healthSlider.maxValue = _unit.maxHP;
        healthSlider.value = _unit.currentHP;
    }

    public void SetHP(int _hp)
    {
        healthSlider.value = _hp;
    }
}
