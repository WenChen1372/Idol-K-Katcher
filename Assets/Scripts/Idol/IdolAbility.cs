using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdolAbility
{
    #region Inspector Variables
    [SerializeField]
    [Tooltip("Ability name as string")]
    private string abilityName;
    public string AbilityName
    {
        get; 
    }

    [SerializeField]
    [Tooltip("Power (magnitude) of ability as float")]
    private float abilityPower;
    public float AbilityPower
    {
        get; 
    }

    [SerializeField]
    [Tooltip("Stamina cost of ability as float")]
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
        abilityName = name;
        abilityPower = power;
        abilityCost = cost; 
    }
    #endregion 
}
