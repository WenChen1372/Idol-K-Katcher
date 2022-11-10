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
    

    //set the HUD
    public void setHUD(IdolClass idol)
    {
        idolNameText.text = idol.IdolName;
        idolTierText.text = idol.IdolTier.ToString();
        hpSlider.maxValue = idol.Health;
        hpSlider.value = idol.CurHealth;
        staminaSlider.maxValue = idol.Stamina;
        staminaSlider.value = idol.CurStamina;
        aegyoDmg.text = "DMG: " + idol.IdolAbilities[0].AbilityPower.ToString();
        aegyoCost.text = "Mana: " + idol.IdolAbilities[0].AbilityCost.ToString();
        singDmg.text = "DMG: " + idol.IdolAbilities[1].AbilityPower.ToString();
        singCost.text = "Mana: " + idol.IdolAbilities[1].AbilityCost.ToString();
        danceDmg.text = "DMG: " + idol.IdolAbilities[2].AbilityPower.ToString();
        danceCost.text = "Mana: " + idol.IdolAbilities[2].AbilityCost.ToString();
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
