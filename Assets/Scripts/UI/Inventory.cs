using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Inventory inventory;

    private List<IdolClass> cardList = new List<IdolClass>();

    public GameObject cardHolderPrefab;

    public Transform grid;

    private PlayerInventory playerInventory;



    //private List<GameObject> cardListHolder = new List<GameObject>();

    //in our code we want it so that depending on the Player Manager player collection we want to add it to our inventory to show;
    //when you select a card and you have enough for upgrade you want the the ecchange button to highlight

    


    // Start is called before the first frame update
    void Start()
    {
        inventory = this;
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        FillInventory();

    }

    void FillInventory()
    {

        cardList = playerInventory.allIdols();

        for (int i = 0; i < cardList.Count; i++)
            {
                GameObject cardHolder = Instantiate(cardHolderPrefab, grid, false);
                IdolClass idol = cardList[i];
                IdolCardHolder holderScript = cardHolder.GetComponent<IdolCardHolder>();

                holderScript.idolImage.sprite = idol.IdolPhotoCard;
                holderScript.idolCount.GetComponent<Text>().text = "Amount " + idol.Count; 

            }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
