using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdolClass : MonoBehaviour
{
   
    #region Protected Variables
    //The 2D sprite of the idol
    protected Sprite idolSprite;
    //The tier of the idol (S, A, B, C) 
    protected char idolTier;
    //The health stat of the idol 
    protected float idolHealth;
    //The stamina stat of the idol
    protected float idolStamina; 
    #endregion 
}
