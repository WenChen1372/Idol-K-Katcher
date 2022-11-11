using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIdolClass : IdolClass
{
    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)
    private void Awake()
    { 
        Health = 50;
        Stamina = 50;
        IdolTier = 'C'; 
        ResetHealth();
        ResetStamina(); 
    }
    #endregion 
}
