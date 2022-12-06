using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class Inventory : MonoBehaviour, IDataPersistance
{




    //idols prefabs 1
    public List<GameObject> cardList;

    public GameObject cardHolderPrefab;

    public Transform grid;

    private PlayerInventory playerInventory;

    //temp inventory count dicitonary
    private Dictionary<string, int> tempInventoryCount; 

    

    // Start is called before the first frame update
    void Start()
    {
        
        FillInventory();

    }

    void FillInventory()
    {


        for (int i = 0; i < cardList.Count; i++)
            {
                GameObject cardHolder = Instantiate(cardHolderPrefab, grid, false);

                GameObject idol = cardList[i];

                IdolClass idolClass = idol.GetComponent<IdolClass>();

                Animator idolAnimation = idol.GetComponent<Animator>();
    

                //get the photo

                Sprite photocard = idolClass.getPhotoCard();

                Sprite animation = idol.GetComponent<SpriteRenderer>().sprite;


                IdolCardHolder holderScript = cardHolder.GetComponent<IdolCardHolder>();
                

                holderScript.setCard(photocard);

                holderScript.setAnimation(animation, idolAnimation, idolClass.IdolTier);
                holderScript.setAnimation(animation, idolAnimation, idolClass.IdolTier);

                


                
                //if dicitonary is empty (new game) set counts to 0
                //otherwise use Inventory Count Dictionary
                if (tempInventoryCount.Count == 0)
                {
                holderScript.setCount(idolClass.Count);
                }
                else
                {
                holderScript.setCount(tempInventoryCount.ElementAt(i).Value); 
                }

            }

    
    }

    #region IDataPersistance
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data)
    {
        tempInventoryCount = data.playerInventoryCount; 
    }

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data)
    {
        data.playerInventoryCount = tempInventoryCount; 
    }
    #endregion 
}
