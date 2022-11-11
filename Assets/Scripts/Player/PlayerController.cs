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
                        SceneManager.LoadScene("Cham");
                    }
                    else
                    {
                        SceneManager.LoadScene("BattleSimulator"); 
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