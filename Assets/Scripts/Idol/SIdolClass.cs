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
        CurHealth = 400;
        CurStamina = 400;
        
    }
    #endregion
}

