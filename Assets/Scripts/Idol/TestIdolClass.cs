using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used to test out different values of stats for idols
//Now can edit through inspector
public class TestIdolClass : IdolClass
{
    #region Inspector Variables
    [SerializeField]
    [Tooltip("Health stat of idol")]
    private float m_Health;

    [SerializeField]
    [Tooltip("Stamina stat of idol")]
    private float m_Stamina;

    [SerializeField]
    [Tooltip("Tier of idol (S, A, B, C) (char)")]
    private char m_Tier;

    [SerializeField]
    [Tooltip("Power of Aeygo Ability (if percentage, make 0.0 - 1.0)")]
    private float m_AeygoPower;

    [SerializeField]
    [Tooltip("Cost of Aeygo Ability")]
    private float m_AeygoCost;

    [SerializeField]
    [Tooltip("Power of Dance Ability")]
    private float m_DancePower;

    [SerializeField]
    [Tooltip("Cost of Dance Ability")]
    private float m_DanceCost;

    [SerializeField]
    [Tooltip("Power of Sing Ability")]
    private float m_SingPower;

    [SerializeField]
    [Tooltip("Cost of Sing Ability")]
    private float m_SingCost; 


    #endregion

    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)
    private void Awake()
    {
        Health = m_Health;
        Stamina = m_Stamina;
        IdolTier = m_Tier;
        IdolAbilities = new IdolAbility[] { new IdolAbility("Aegyo", m_AeygoPower, m_AeygoCost), new IdolAbility("Dance", m_DancePower, m_DanceCost), new IdolAbility("Sing", m_SingPower, m_SingCost) };
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
