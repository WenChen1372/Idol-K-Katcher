using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour, IDataPersistance
{
    [SerializeField]
    public Animator playerChoiceHandlerAnimation, choiceAnimation;

    [SerializeField]
    public float reactionTime;

    public float trueTimer;

    [SerializeField]
    public float maxTime;

    public bool stillGoing = true;

    [SerializeField]
    public Button left, right, down, up;

    private GameplayController gameplayController;

    //inventory count dictionary
    private Dictionary<string, int> tempInventoryCount = new Dictionary<string, int>();

    //player xp
    private int tempXP;

    //player training points
    private int tempTrainingPoints;

    //player tier
    private char tempTier;

    //player name
    private string tempName;


    #region IDataPersistance
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data)
    {
        tempInventoryCount = data.playerInventoryCount;
        Debug.Log(tempInventoryCount.Count);
        tempXP = data.playerXP;
        tempTrainingPoints = data.playerTrainingPoints;
        tempTier = data.playerCurTier;
        tempName = data.playerCurName;
    }

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data)
    {
        data.playerTrainingPoints = tempTrainingPoints;
        data.playerXP = tempXP;
        data.playerInventoryCount = tempInventoryCount;
        data.playerCurTier = tempTier;
        data.playerCurName = tempName; 
    }
    #endregion 
    public void Start()
    {
        left.enabled = false;
        right.enabled = false;
        down.enabled = false;
        up.enabled = false;
        gameplayController = GetComponent<GameplayController>();
        StartCoroutine(AnimatingStart());
        reactionTime = 0f;
        trueTimer = 0f;
    }

    public void Update()
    {
        if (stillGoing) { 
            reactionTime += Time.deltaTime;
            if (reactionTime > 6.5)
            {
                trueTimer += Time.deltaTime;
           
                if (trueTimer > maxTime)
                {
                    playerChoiceHandlerAnimation.Play("lose");
                    //choiceAnimation.Play("losingexit");
                    choiceAnimation.Play("ExitButton");
                    print("Stoppedtrue " + trueTimer);
                    stillGoing = false;
                }

            }
        }

    }

    public IEnumerator AnimatingStart()
    {
        yield return new WaitForSeconds(2f);

        choiceAnimation.Play("showchamz");

        yield return new WaitForSeconds(4f);

        choiceAnimation.Play("showopp");

        yield return new WaitForSeconds(0.5f);

        playerChoiceHandlerAnimation.Play("showholder");
        //print(reactionTime);

        left.enabled = true;
        right.enabled = true;
        down.enabled = true;
        up.enabled = true;
    }


    public void PlayerMadeChoice()
    {
        print(reactionTime);
        playerChoiceHandlerAnimation.Play("RemoveHandler");
       
        choiceAnimation.Play("showplayer");
        choiceAnimation.Play("ExitButton");
        ShowWinner();
        
    }

    public void ShowWinner()
    {
        if (gameplayController.Opponent_Choice != gameplayController.player_Choice)
        {
            playerChoiceHandlerAnimation.Play("win");
            stillGoing = false;
            if (tempTier == 'S')
            {
                tempTrainingPoints += 4000;
                tempXP += 400;
                tempInventoryCount[tempName + tempTier] += 1;
            }
            else if (tempTier == 'A')
            {
                tempTrainingPoints += 3000;
                tempXP += 300;
                tempInventoryCount[tempName + tempTier] += 1;
            }
            else if (tempTier == 'B')
            {
                tempTrainingPoints += 2000;
                tempXP += 200;
                tempInventoryCount[tempName + tempTier] += 1;
            }
            else
            {
                tempTrainingPoints += 1000;
                tempXP += 100;
                tempInventoryCount[tempName + tempTier] += 1;
            }
            print("Stopped win" + trueTimer);
        }
        else
        {
            playerChoiceHandlerAnimation.Play("lose");
            stillGoing = false;
            print("Stopped loss" + trueTimer);
        }
        Debug.Log(tempTrainingPoints);
        
    }


    // this is the actual end to the game that displays the animations of win or lose
}