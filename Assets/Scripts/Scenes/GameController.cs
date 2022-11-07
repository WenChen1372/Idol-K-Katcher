using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    #region Scene Transition Methods
    private void LoadMapScene()
    {
        SceneManager.LoadSceneAsync("Location-basedGame");
    }

    private void LoadInventory()
    {
        SceneManager.LoadSceneAsync("Collection");
    }

    private void LoadCham()
    {
        SceneManager.LoadSceneAsync("Cham"); 
    }

    private void LoadBattle()
    {
        SceneManager.LoadSceneAsync("BattleSimulator"); 
    }

    private void LoadTrading()
    {
        //SceneManager.LoadSceneAsync(""); 
    }
    #endregion 
}
