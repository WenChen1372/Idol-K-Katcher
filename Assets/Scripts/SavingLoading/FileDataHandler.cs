using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO; 


public class FileDataHandler
{
    #region Private Variables
    //name of file path to save file to
    private string dataDirPath = "";

    //name of file to save data to
    private string dataFileName = "";
    #endregion

    #region constructor
    public FileDataHandler(string path, string fileName)
    {
        dataDirPath = path;
        dataFileName = fileName; 
    }
    #endregion

    #region Load/Save Methods
    public GameData Load()
    {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null; 
        if (File.Exists(fullPath))
        {
            try
            {
                //Load serialized data from file
                string dataToLoad = ""; 
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); 
                    }
                }

                //deserialize data from Json to GameData object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad); 
            }
            catch (Exception e)
            {
                Debug.LogError("Error occcured whent trying to load data from file: " + fullPath + "\n" + e); 
            }
        }

        return loadedData; 
    }

    public void Save(GameData data)
    {
        //use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //create directory the file will be written to if it doesn't already exist 
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize C# game data object into JSON 
            string dataToStore = JsonUtility.ToJson(data, true); 

            //write serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore); 
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e); 
        }
    }
    #endregion 
}
