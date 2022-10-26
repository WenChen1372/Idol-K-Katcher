using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIdolClass : IdolClass
{
    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)
    private void Awake()
    {
        Health = 100;
        Stamina = 100;
        IdolTier = 'B';
        IdolAbilities = new IdolAbility[] { new IdolAbility("Aegyo", 0.4f, 20), new IdolAbility("Dance", 20, 15), new IdolAbility("Sing", 50, 40) };
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
