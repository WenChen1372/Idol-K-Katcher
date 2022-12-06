using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameChoices
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT

}

public class GameplayController : MonoBehaviour
{
    //no need to show in inspector, will set in code
    [SerializeField]
    private Sprite up_Sprite, down_Sprite, right_Sprite, left_Sprite;

    [SerializeField]
    public Image playerChoice_Img, oponnentChoice_Img;

    [SerializeField]
    private Sprite[] spriteArray;

    [SerializeField]
    private TMP_Text infoText;

    

    //current name of idol
    private string tempName;
    //current tier of idol
    private char tempTier; 


    

    private bool clockisTicking;

    public GameChoices player_Choice = GameChoices.NONE, Opponent_Choice = GameChoices.NONE;

    private AnimationController animationController;

    #region Helper Methods
    private void SetArrowDictionary()
    {
        int i = 0;
        int j = 0;
        while (j < nameArray.Length)
        {
            arrowDictionary.Add(nameArray[j], new Sprite[] {spriteArray[i], spriteArray[i+1] , spriteArray[i+2] , spriteArray[i+3] });
            i += 4;
            j += 1; 
        }
    }

    private void SetDirections()
    {
        string key = tempName + tempTier;
        Debug.Log(key);
        Sprite[] arrows = arrowDictionary[key];
        up_Sprite = arrows[0];
        Debug.Log(up_Sprite);
        down_Sprite = arrows[1];
        right_Sprite = arrows[2];
        left_Sprite = arrows[3]; 
    }
    #endregion 

    #region IDataPersistance
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data)
    {
        Debug.Log(data.playerCurName);
        Debug.Log(data.playerCurTier);
        tempName = data.playerCurName;
        tempTier = data.playerCurTier;
        SetArrowDictionary();
        SetDirections();
    }

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data)
    {
        data.playerCurName = tempName;
        data.playerCurTier = tempTier; 
    }
    #endregion

    void Awake()
    {
        animationController = GetComponent<AnimationController>();
    }

    public void SetChoices(GameChoices gameChoices)
    {
        switch(gameChoices)
        {
            case GameChoices.RIGHT:

                playerChoice_Img.sprite = right_Sprite;

                player_Choice = GameChoices.RIGHT;

                break;

            case GameChoices.LEFT:

                playerChoice_Img.sprite = left_Sprite;

                player_Choice = GameChoices.LEFT;

                break;

            case GameChoices.UP:

                playerChoice_Img.sprite = up_Sprite;

                player_Choice = GameChoices.UP;

                break;

            case GameChoices.DOWN:

                playerChoice_Img.sprite = down_Sprite;

                player_Choice = GameChoices.DOWN;

                break;
        }
       
    }
    public void SetOpponentChoice()
    {
        int rnd = Random.Range(0, 3);

        switch(rnd)
        {
            case 0:

                Opponent_Choice = GameChoices.RIGHT;

                oponnentChoice_Img.sprite = right_Sprite;

                break;

            case 1:

                Opponent_Choice = GameChoices.LEFT;

                oponnentChoice_Img.sprite = left_Sprite;

                break;

            case 2:

                Opponent_Choice = GameChoices.UP;

                oponnentChoice_Img.sprite = up_Sprite;

                break;

            case 3:
                Opponent_Choice = GameChoices.DOWN;

                oponnentChoice_Img.sprite = down_Sprite;

                break;
        }
    }
    //public void DetermineWinner()
    //{
    //    print("Testing");
    //    if (player_Choice == Opponent_Choice || animationController.reactionTime > 5.0)
    //    {
    //        //infoText.text = "LOSER ";
    //        //StartCoroutine(animationController.DisplayWinner());
    //        //animationController.playerChoiceHandlerAnimation.Play("win");
    //        return;
    //    }

    //    if (player_Choice != Opponent_Choice)
    //    {
    //        //infoText.text = "WINNER";

    //        StartCoroutine(DisplayWinnerAndRestart());

    //        return;
    //    }
        

    //}

    //IEnumerator DisplayWinnerAndRestart()
    //{
    //    yield return new WaitForSeconds(2f);

    //    infoText.gameObject.SetActive(true);

    //    yield return new WaitForSeconds(2f);

    //    infoText.gameObject.SetActive(false);

    //    animationController.ResetAnimations();
    //}
}




//Jonas its this one to mess around with 