using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdolClass : IdolClass
{
    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)
    private void Awake()
    {
        Health = 200;
        Stamina = 200;
        IdolTier = 'A';
        IdolAbilities = new IdolAbility[] { new IdolAbility("Aegyo", 0.7f, 30), new IdolAbility("Dance", 35, 30), new IdolAbility("Sing", 80, 70) };
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
