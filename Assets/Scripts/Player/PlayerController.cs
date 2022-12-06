using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq; 

public class PlayerController : MonoBehaviour, IDataPersistance
{
    #region Inspector Variables
    [SerializeField]
    [Tooltip("dictionary of idols with (key) idolName+Tier and (value) prefab1")]
    private Dictionary<string, GameObject> prefabIdols = new Dictionary<string, GameObject>();

    [SerializeField]
    [Tooltip("dictionary of idols with (key) idolName+Tier and (value) count for inventory")]
    private Dictionary<string, int> inventoryCount = new Dictionary<string, int>();

    [SerializeField]
    [Tooltip("list of key names for dictionary")]
    private string[] nameArray;

    [SerializeField]
    [Tooltip("list of prefab1 for dictionary")]
    private GameObject[] prefabArray; 

    [SerializeField]
    [Tooltip("player xp amount to level up")]
    private int upgradeXp;
    public int UpgradeXp
    {
        get; 
    }
    #endregion 
    #region Private Variables
    //player level
    private int level; 
    public int Level
    {
        get;
    }

    //player XP
    private int xp;
    public int Xp
    {
        get;
    }

    //player Training Points
    private int trainingPoints; 
    public int TrainingPoints
    {
        get;
    }

    //current idol being catched or battled
    private GameObject curIdol;
    public GameObject CurIdol
    {
        get; 
    }

    //curent tier of idol being catched or battled
    private char curTier; 
    public char CurTier
    {
        get; 
    }

    //current name of idol being cathced or battled
    private string curName; 
    public string CurName
    {
        get; 
    }
    #endregion

    #region 

    #endregion 
    #region Trigger Methods
    void OnTriggerStay(Collider other)
    {
        //Only accounts for 1 touch
        //Will not consider multiple touches
        if (other.CompareTag("Idol") || other.CompareTag("IdolBattle"))
        {
            //Only accounts for 1 touch
            //Will not consider multiple touches
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (other.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (other.CompareTag("Idol"))
                    {
                        curIdol = other.gameObject;
                        curTier = other.gameObject.GetComponent<IdolClass>().IdolTier;
                        Debug.Log(curTier);
                        curName = other.gameObject.GetComponent<IdolClass>().getIdolName();
                        Debug.Log(curName);
                        //save game anywhere before we load a scene
                        DataPersistanceManager.instance.SaveGame();
                        SceneManager.LoadSceneAsync("Cham");
                    }
                    else
                    {
                        Debug.Log("reach statment");
                        curIdol = RandomIdol();
                        Debug.Log(curIdol);
                        curTier = curIdol.GetComponent<IdolClass>().IdolTier;
                        curName = curIdol.GetComponent<IdolClass>().IdolName;
                        //save game anywhere before we load a scene
                        DataPersistanceManager.instance.SaveGame();
                        SceneManager.LoadSceneAsync("BattleSimulator");

                    }
                }
            }
        }
    }
    #endregion

    #region Setter Methods
    //change player level by a certain amount
    public void ChangeLevel (int amount)
    {
        level = amount; 
    }

    //change player xp by a certain amount
    public void ChangeXP(int amount, int levelAmount)
    {
        xp = amount;
        if (xp >= upgradeXp)
        {
            ChangeLevel(xp / upgradeXp + levelAmount);
            xp = xp % upgradeXp; 
        }
        else
        {
            level = levelAmount; 
        }
    }

    //change player training points by a certain amount
    public void ChangeTrainingPoints(int amount)
    {
        trainingPoints += amount;
    }
    #endregion

    #region IDataPersistance Methods
    public void LoadData(GameData data)
    {
        //changes level and xp
        if(data.playerXP == 0 && data.playerLevel == 0)
        {
            level = data.playerLevel;
            xp = data.playerXP; 
        }
        else
        {
            ChangeXP(data.playerXP, data.playerLevel);
        }
        trainingPoints = data.playerTrainingPoints;
        curIdol = data.playerCurIdol;
        curTier = data.playerCurTier;
        curName = data.playerCurName;
        //This is to account for the first ever load, if we create a new game dictionaries length = 0
        //in this case we set up the dictionaries with helper methods
        //If dictionaries from game data is not 0, theres stuff, then load it in
        Debug.Log(nameArray[0]);
        if (data.playerPrefabIdols.Count != 0)
        {
            prefabIdols = data.playerPrefabIdols;
        }
        else
        {
            Debug.Log("this is else statement");
            SetPrefabDictionary();
            Debug.Log(prefabIdols);
        }
        if (data.playerInventoryCount.Count != 0)
        {
            inventoryCount = data.playerInventoryCount;
        } 
        else
        {
            SetInventoryDictionary();
        }
    }

    public void SaveData(GameData data)
    {
        data.playerLevel = level;
        data.playerXP = xp;
        data.playerTrainingPoints = trainingPoints;
        data.playerCurIdol = curIdol;
        data.playerCurTier = curTier;
        data.playerCurName = curName;
        data.playerPrefabIdols = prefabIdols;
        data.playerInventoryCount = inventoryCount; 
    }
    #endregion

    #region Random Idol Methods
    private GameObject RandomIdol()
    {
        //for showcase, you have equal chance to battle anyone
        //for future implementations, change percentage chances
        int idolNum = Random.Range(0, prefabIdols.Count - 1);
        return prefabIdols.ElementAt(idolNum).Value; 
    }
    #endregion

    #region Helper Methods
    private void SetPrefabDictionary()
    {
        Debug.Log(nameArray.Length);
        for (int i = 0; i < nameArray.Length; i++)
        {
            Debug.Log("adding to dictionary");
            prefabIdols.Add(nameArray[i], prefabArray[i]);
            Debug.Log(prefabIdols.Count);
        }
    }

    private void SetInventoryDictionary()
    {
        for (int i = 0; i < nameArray.Length; i++)
        {
            inventoryCount.Add(nameArray[i], 0);
        }
    }
    #endregion 
}