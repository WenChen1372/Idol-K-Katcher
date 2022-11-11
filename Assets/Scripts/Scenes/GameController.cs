using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    #region Scene Transition Methods
    public void LoadMapScene()
    {
        SceneManager.LoadSceneAsync("Location-basedGame");
    }

    public void LoadInventory()
    {
        SceneManager.LoadSceneAsync("Collection");
    }

    public void LoadCham()
    {
        SceneManager.LoadSceneAsync("Cham"); 
    }

    public void LoadBattle()
    {
        SceneManager.LoadSceneAsync("BattleSimulator"); 
    }

    public void LoadTrading()
    {
        //SceneManager.LoadSceneAsync(""); 
    }
    #endregion

    #region Quit Methods
    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void LoadHome()
    {
        SceneManager.LoadSceneAsync("MainMenu"); 
    }
    #endregion 
}
