using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator playerChoiceHandlerAnimation, choiceAnimation;

    [SerializeField]
    public float reactionTime;

    public float trueTimer;

    [SerializeField]
    public float maxTime;

    public bool clockisTicking;

    private GameplayController gameplayController;

    public void Start()
    {
        
      
        gameplayController = GetComponent<GameplayController>();
        StartCoroutine(AnimatingStart());
        reactionTime = 0f;
        trueTimer = 0f;
    }

    public void Update()
    {

        reactionTime += Time.deltaTime;
        if (reactionTime > 6.5)
        {
            trueTimer += Time.deltaTime;
            print(trueTimer);
            if (trueTimer > 1.0)
            {
                playerChoiceHandlerAnimation.Play("lose");
                choiceAnimation.Play("ExitButton");
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
        print(reactionTime);
        
    }

    public void ResetAnimations()
    {
        playerChoiceHandlerAnimation.Play("ShowHandler");
        choiceAnimation.Play("RemoveChoices");
    }

    public void PlayerMadeChoice()
    {
        print(reactionTime);
        playerChoiceHandlerAnimation.Play("RemoveHandler");
       
        choiceAnimation.Play("showplayer");
        choiceAnimation.Play("ExitButton");
        ShowWinner();

        //playerChoiceHandlerAnimation.Play("win");
        print(reactionTime);
        
    }

    public void ShowWinner()
    {
        if (gameplayController.Opponent_Choice != gameplayController.player_Choice)
        {
            playerChoiceHandlerAnimation.Play("win");
        }
        else
        {
            playerChoiceHandlerAnimation.Play("lose");
        }
        
        //yield return new WaitForSeconds(0.2f);
    }
}