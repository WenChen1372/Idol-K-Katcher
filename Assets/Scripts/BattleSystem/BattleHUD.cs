using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    public Text idolNameText;
    public Text idolTierText;
    public Slider hpSlider;
    public Slider staminaSlider;
    public Text aegyoDmg;
    public Text aegyoCost;
    public Text singDmg;
    public Text singCost;
    public Text danceDmg;
    public Text danceCost;

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
        maxHealth = idol.Health;
        curHealth = idol.CurHealth;
        maxStamina = idol.Stamina;
        curStamina = idol.CurStamina;
        staminaSlider.maxValue = idol.Stamina;
        staminaSlider.value = idol.CurStamina;
        IdolAbility[] save = idol.getIdolAbility();
        aegyoDmg.text = "DMG: " + save[0].getAbilityPower().ToString();
        Debug.Log(save[0].getAbilityPower().ToString());
        aegyoCost.text = "Mana: " + save[0].getAbilityCost().ToString();
        singDmg.text = "DMG: " + save[1].getAbilityPower().ToString();
        singCost.text = "Mana: " + save[1].getAbilityCost().ToString();
        danceDmg.text = "DMG: " + save[2].getAbilityPower().ToString();
        danceCost.text = "Mana: " + save[2].getAbilityCost().ToString();
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
