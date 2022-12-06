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

    //current idol interacting with
    //saved in PlayerController
    //used(loaded) in battle and cham
    public GameObject playerCurIdol;

    //cuurent tier of idol interacting with
    //saved in PlayerController
    //used(loaded) in battle and cham 
    public char playerCurTier;

    //current name of idol ineracting with
    public string playerCurName; 

    //player chosen in character seledction
    public GameObject playerSelection;

    //player prefabIdol dictionary 
    public Dictionary<string, GameObject> playerPrefabIdols;

    //player inventory count of idol dictionary
    public Dictionary<string, int> playerInventoryCount;
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
        playerCurIdol = null;
        playerCurTier = 'N';
        playerCurName = "null"; 
        playerSelection = null;
        playerPrefabIdols = new Dictionary<string, GameObject>();
        playerInventoryCount = new Dictionary<string, int>(); 
    }
    #endregion 
}
