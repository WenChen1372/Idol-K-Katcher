using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Inventory inventory;

    public List<IdolCard> cardList = new List<IdolCard>();

    //private List<GameObject> cardListHolder = new List<GameObject>();

    //in our code we want it so that depending on the Player Manager player collection we want to add it to our inventory to show;
    //when you select a card and you have enough for upgrade you want the the ecchange button to highlight


    //cardList = PlayerManager.playerCollection;
    


    // Start is called before the first frame update
    void Start()
    {
        //inventory = this;
        //FillInventory()

    }

    void FillInventory()
    {
        //for (int i = 0; i < cardList.Count; i++)
        //{
            //GameObject cardHolder = Instantiate(itemHolderPrefab, grid?, false?);
            //IdolCardHolder holderScript = cardHolder.GetComponent;

            //holderScript.itemID = cardList[i].itemID???
            //holderScript.GetComponent<SelectIdol>().itemID = cardList[i].itemeID;
            //cardListHolder.Add(cardHolder);
            //if (cardList[i].selected) {
                //we want the echange option to be able to be selected

            //}  

        //}

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
