using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{




    
    public List<GameObject> cardList;

    public GameObject cardHolderPrefab;

    public Transform grid;

    private PlayerInventory playerInventory;

    

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

                holderScript.setCount(idolClass.Count);

                



            }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
