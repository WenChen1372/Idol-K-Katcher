using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdolAbility
{
    #region Private Variables
    //The name of the ability
    private string abilityName;
    public string AbilityName
    {
        get;
    }

    //The power (magnitude) of the ability (damage, stun duration, etc.) 
    //float can represent damage, seconds of stun, or percentage (percentage should be 0.0 - 1.0) 
    private float abilityPower;
    public float AbilityPower
    {
        get;
    }

    //The stamina cost of the ability
    private float abilityCost;
    public float AbilityCost
    {
        get;
    }
    #endregion

    #region Constructor Method
    //constructor
    public IdolAbility(string name, float power, float cost)
    {
        AbilityName = name;
        AbilityPower = power;
        AbilityCost = cost; 
    }
    #endregion 
}
