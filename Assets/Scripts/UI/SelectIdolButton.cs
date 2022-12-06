using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SelectIdolButton : MonoBehaviour, IDataPersistance
{

    //public int itemID;
	public Text idolCount;
	public Image idolCard;
    public Image idolSprite;
    
    public GameObject idol;


    
    public void setCard(Sprite card)
    {
        idolCard.sprite = card;
    }

    public void setIdol(GameObject idolObject)
    {
        idol = idolObject;
    }

    public void setAnimation(Animator ani, char tier)
    {

        RuntimeAnimatorController aniControl = ani.runtimeAnimatorController;
        Animator thisAni = idolSprite.GetComponent<Animator>();
        thisAni.runtimeAnimatorController = aniControl;


        if (tier != 'C') {

            thisAni.SetBool("inInventory", true);

        }

        
        

    }


    #region IData
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data)
    {
        return; 
    }

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data)
    {
        Debug.Log(data.playerSelection);
        data.playerSelection = idol;
        Debug.Log(data.playerSelection);
    }

    public void OnClick()
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("BattleSimulator"); 
    }
    #endregion

    public void setCount(int count)
    {
        idolCount.text = count.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
