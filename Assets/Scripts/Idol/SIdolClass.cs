using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIdolClass : IdolClass
{
    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)
    private void Awake()
    {
        Health = 400;
        Stamina = 400;
        IdolTier = 'S';
        Count = 0;
        IdolAbilities = new IdolAbility[] { new IdolAbility("Aegyo", 0.9f, 40), new IdolAbility("Dance", 50, 45), new IdolAbility("Sing", 100, 90) };
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
