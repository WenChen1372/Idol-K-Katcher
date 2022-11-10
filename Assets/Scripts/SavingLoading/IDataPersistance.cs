using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data);

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data); 
}
