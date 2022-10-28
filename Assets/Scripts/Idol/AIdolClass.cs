using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdolClass : IdolClass
{
    #region Initilization 
    //I wanted to use a constructor here, but Unity says
    //to use awake instead for MonoBehavior classes
    //(Idol Class is a MonoBehavior subclass)

    

    public string IdolNameTest;

    public string getIdolNameTest()
    {
        return IdolNameTest;
    } 

    private void Awake()
    {

        Health = 200;
        CurHealth = 200;
        Stamina = 200;
        CurStamina = 9999;
        IdolTier = 'A';
        IdolAbilities = new IdolAbility[] { new IdolAbility("Aegyo", 0.7f, 30), new IdolAbility("Dance", 35, 30), new IdolAbility("Sing", 80, 70) };
        ResetHealth();
        ResetStamina();
        updateName();
    }

    private void Update()
    {
        updateName();
    }

    private void updateName()

    {
        IdolName = IdolNameTest;
    }

    
    #endregion
}
