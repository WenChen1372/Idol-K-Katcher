using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    public Text idolNameText;
    public Text idolTierText;
    public Slider hpSlider;
    public Slider staminaSlider;
    

    //set the HUD
    public void setHUD(IdolClass idol)
    {
        idolNameText.text = idol.getIdolName();
        idolTierText.text = idol.IdolTier.ToString();
        hpSlider.maxValue = idol.Health;
        hpSlider.value = idol.CurHealth;
        staminaSlider.maxValue = idol.Stamina;
        staminaSlider.value = idol.CurStamina;

    }


    public void SetHp(float hp)
    {
        hpSlider.value = hp;
    }

    public void SetStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }
}
