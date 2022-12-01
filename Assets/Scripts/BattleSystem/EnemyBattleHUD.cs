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

    public Text hpText;
    public Text staminaText;


    float maxHealth;
    float curHealth;
    float maxStamina;
    float curStamina;
    

    //set the HUD
    public void setHUD(IdolClass idol)
    {
        idolNameText.text = idol.getIdolName();
        idolTierText.text = idol.IdolTier.ToString();
        hpSlider.maxValue = idol.Health;
        hpSlider.value = idol.CurHealth;
        staminaSlider.maxValue = idol.Stamina;
        staminaSlider.value = idol.CurStamina;

        maxHealth = idol.Health;
        curHealth = idol.CurHealth;
        maxStamina = idol.Stamina;
        curStamina = idol.CurStamina;

        hpText.text = curHealth.ToString() + "/" + maxHealth.ToString();
        staminaText.text = curStamina.ToString() + "/" + maxStamina.ToString();

    }


    public void SetHp(float hp, IdolClass idol)
    {
        hpSlider.maxValue = idol.Health;
        hpSlider.value = hp;
        hpText.text = idol.CurHealth.ToString() + "/" + idol.Health.ToString();
    }

    public void SetStamina(float stamina, IdolClass idol)
    {
        staminaSlider.value = stamina;
        staminaText.text = idol.CurStamina.ToString() + "/" + idol.Stamina.ToString();
    }
}
