using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdolClass : MonoBehaviour
{
    #region Private Variables
    //The health stat of the idol
    private float health;
    public float Health
    {
        get;
        set;
    }

    //The stamina stat of the idol 
    private float stamina;
    public float Stamina
    {
        get;
        set;
    }

    //The name of the Idol
    
    [SerializeField]
    private string idolname;
    public string IdolName
    {
        get;
        set;
    }

    //The tier of the idol (S, A, B, C) 
    private char idolTier; 
    public char IdolTier
    {
        get;
        set;
    }

    //The amount of copies we have of this idol
    private int count;
    public int Count
    {
        get;
        set;
    }

    //List of Abilites of the Idol 
    private IdolAbility[] idolAbilities;
    public IdolAbility[] IdolAbilities
    {
        get;
        set; 
    }

    //The current health of the idol
    //Set through other methods
    private float curHealth;
    public float CurHealth
    {
        get;
        set;
    }

    //The current stamina of the idol
    //Set through other methods
    private float curStamina; 
    public float CurStamina
    {
        get;
        set;
    }
    #endregion

    #region Inspector Variables
    //The 2D sprite of the associated photocard with the idol
    //This is an inspector variable so it is easier to just drag and drop...
    //The photocard for the idol in the editor
    //Doesn't need setter, since will be handled in inspector
    [SerializeField]
    [Tooltip("The 2D sprite of the associated photocard with the idol")]
    private Sprite idolPhotoCard;
    public Sprite IdolPhotoCard
    {
        get;
    }
    #endregion


    #region Health/Stamina Methods
    //These two methods reset the current health/stamina to the original health and stamina stat of the idol
    //This is good to be used before a battle scene to make sure idol is full health and stamina
    public void ResetHealth()
    {
        curHealth = health;
    }

    public void ResetStamina()
    {
        curStamina = stamina; 
    }

    //These two methods change the current health/stamina by a certain amount
    //This is good to be used when getting attacked/using attacks in battle 
    //Can be positive or negative
    public bool ChangeHealth(float amount)
    {
        CurHealth -= amount; 

        if (CurHealth <=0)
        {
            return true;
        }

        else 
        {
            return false;
        }
    }

    public void ChangeStamina(float amount)
    {
        curStamina -= amount; 
    }
    #endregion 
}
