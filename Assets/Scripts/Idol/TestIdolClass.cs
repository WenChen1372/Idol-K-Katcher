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
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
