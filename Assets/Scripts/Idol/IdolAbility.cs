using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdolAbility : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    [Tooltip("The magnitude of the ability (damange, seconds of stun, etc")]
    private float abilityDamage;
    public float AbilityDamage
    {
        get; 
    }

    [SerializeField]
    [Tooltip("The stamina cost of the ability")]
    private float abilityCost;
    public float AbilityCost
    {
        get; 
    }
    #endregion
}
