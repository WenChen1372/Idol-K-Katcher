using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Game Data Needed In-Between Game Sessions (Save Data) 
    //player XP to save from Map 
    public int playerXP;

    //player Level to save from Map 
    public int playerLevel; 
    
    //player Training Points to save from Map
    public int playerTrainingPoints;

    //player collection of Idols to save from Inventory/Trading
    //look to PlayerInventory script (Wen) 
    public Dictionary<string, Dictionary<char, IdolClass>> playerIdolCollection;
    #endregion

    //values defined in constructor acts as default values
    //the game starts a new game when there is no data to load
    #region Constructor (Initialize New Game Data) 
    public GameData()
    {
        playerXP = 0;
        playerLevel = 0;
        playerTrainingPoints = 0;
        playerIdolCollection = new Dictionary<string, Dictionary<char, IdolClass>>(); 
    }
    #endregion 
}
