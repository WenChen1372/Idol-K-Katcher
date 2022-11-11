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
        idolNameText.text = idol.getIdolName();
        idolTierText.text = idol.IdolTier.ToString();
        hpSlider.maxValue = idol.Health;
        hpSlider.value = idol.CurHealth;
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
