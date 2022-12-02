using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour, IDataPersistance
{
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

    //player xp amount to level up
    private int upgradeXp; 
    public int UpgradeXp
    {
        get; 
    }

    //struct containing 2 values to keep track of idol object for "Cham" and "Battle" 
    //1.)idol as game object
    //2.)which mode as title of scene
    public struct currIdol
    {
        IdolClass idol;
        string sceneName; 
    }
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
                        //save game anywhere before we load a scene
                        //DataPersistanceManager.instance.SaveGame();
                        SceneManager.LoadSceneAsync("Cham");
                    }
                    else
                    {
                        //save game anywhere before we load a scene
                        //DataPersistanceManager.instance.SaveGame();
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
        level += amount; 
    }

    //change player xp by a certain amount
    public void ChangeXP(int amount)
    {
        xp += amount;
        if (xp >= upgradeXp)
        {
            ChangeLevel(1);
            xp = xp - upgradeXp; 
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
        level = data.playerLevel;
        xp = data.playerXP;
        trainingPoints = data.playerTrainingPoints; 
    }

    public void SaveData(GameData data)
    {
        data.playerLevel = level;
        data.playerXP = xp;
        data.playerTrainingPoints = trainingPoints; 
    }
    #endregion 
}