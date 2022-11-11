using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
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
                    choiceAnimation.Play("321");
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
            print("Stopped win" + trueTimer);
        }
        else
        {
            playerChoiceHandlerAnimation.Play("lose");
            stillGoing = false;
            print("Stopped loss" + trueTimer);
        }
        
    }


    // this is the actual end to the game that displays the animations of win or lose
}