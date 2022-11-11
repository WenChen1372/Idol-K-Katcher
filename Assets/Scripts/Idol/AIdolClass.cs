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
        ResetHealth();
        ResetStamina();
    }
    #endregion
}
