using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdolCardHolder : MonoBehaviour
{


    //public int itemID;
	public Text idolCount;
	public Image idolCard;
    public Image idolAnimation;


    
    public void setCard(Sprite card)
    {
        idolCard.sprite = card;
    }

    public void setAnimation(Sprite idol)
    {
        idolAnimation.sprite = idol;
    }

    public void setCount(int count)
    {
        idolCount.text = count.ToString();
    }








    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
