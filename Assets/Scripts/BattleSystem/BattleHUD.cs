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

    public Text specialMoveName;
    public Text skillDesc;
    
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

        specialMoveName.text = save[3].getAbilityName();
        setSkillDesc(idol);

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

    public void setSkillDesc(IdolClass idol)
    {
        string name = idol.getIdolName();
        char tier = idol.IdolTier;

        if (name == "Rm")
        {
            skillDesc.text = "Regeneration ability";
        }

        else if (name == "V")
        {
            skillDesc.text = "Poison Enemy";
        }

        else if (name == "Jhope")
        {
            skillDesc.text = "Enemy turn Skip";
        }

        else if (name == "Suga")
        {
            if(tier == 'C')
            {
                skillDesc.text = "10% chance to win";
            }

            else if (tier == 'B')
            {
                skillDesc.text = "20% chance to win";
            }

            else if (tier == 'A')
            {
                skillDesc.text = "30% chance to win";
            }

            else if (tier == 'S')
            {
                skillDesc.text = "40% chance to win";
            }
        }

        else if (name == "Jimin")
        {
            skillDesc.text = "Use opponent Special";
        }

        else if (name == "Jk")
        {
            skillDesc.text = "Get Max HP and Stamina";
        }

        else if (name == "Jin")
        {
            skillDesc.text = "Swap Health with Opponent";
        }
        
    }


}
