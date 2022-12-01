using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement; 

public class DataPersistanceManager : MonoBehaviour
{
    #region Inspector Variables
    [Header("File Storage Config")]
    [SerializeField]
    [Tooltip("Name of file to save game data to")]
    private string fileName; 
    #endregion 

    #region Private Variables
    //the current Game Data being used in the game
    private GameData gameData;

    //a list of references to the scripts that implement IDataPersistance Interface, so...
    //that we can call them in Load and Save Game
    private List<IDataPersistance> dataPersistanceObjects;

    //the thing that controls read/write to files (data handler) 
    private FileDataHandler dataHandler; 
    #endregion 

    #region Singleton Variables
    //the ONE SINGLE instance of the Data Persistance Manager
    public static DataPersistanceManager instance
    {
        get;
        private set; 
    }
    #endregion

    #region Initialize
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Data Persistance Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return; 
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    #endregion

    #region Game Methods (New, Load, Save)
    public void NewGame()
    {
        //To create a new game, just create a new GameData Object with default values
        gameData = new GameData(); 
    }

    public void LoadGame()
    {
        //load saved data from a file using the data handler
        gameData = dataHandler.Load(); 

        //if no data to be loaded, just make a new game 
        if (this.gameData == null)
        {
            Debug.Log("No data to be loaded, create new game");
            NewGame(); 
        }

        //push loaded data to all scripts that need it 
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData); 
        }

        //just to see if player on map is working
        Debug.Log("Loaded level, xp, training points: " + gameData.playerLevel + ", " + gameData.playerXP + ", " + gameData.playerTrainingPoints); 
    }

    public void SaveGame()
    {
        //pass data to other scripts to update it 
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(gameData); 
        }

        //just to see if player on map is working
        Debug.Log("Saved level, xp, training points: " + gameData.playerLevel + ", " + gameData.playerXP + ", " + gameData.playerTrainingPoints);

        //save data to a file using data handler 
        dataHandler.Save(gameData);
    }
    #endregion

    #region Quit Game Method
    private void OnApplicationQuit()
    {
        SaveGame(); 
    }
    #endregion

    #region Finding Persistance Scripts  Methods
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().
            OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects); 
    }
    #endregion

    #region SceneLoading Methods
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called");
        dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    #endregion 
}
