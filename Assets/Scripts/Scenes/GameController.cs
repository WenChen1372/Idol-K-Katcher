using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    #region Scene Transition Methods
    public void LoadMapScene()
    {
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Location-basedGame");
    }

    public void LoadInventory()
    {
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Collection");
    }

    public void LoadCham()
    {
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Cham"); 
    }

    public void LoadBattle()
    {
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("BattleSimulator"); 
    }

    public void LoadTrading()
    {
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
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
        //save game anywhere before we load a scene
        //DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu"); 
    }

    public void LoadTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void LoadBattleWin()
    {
        SceneManager.LoadSceneAsync("Battle_Win");
    }

    public void LoadBattleLose()
    {
        SceneManager.LoadSceneAsync("Battle_Loss");
    }

    public void LoadSelectScene()
    {
        SceneManager.LoadSceneAsync("PlayerSelection");
    }
    #endregion 
}
