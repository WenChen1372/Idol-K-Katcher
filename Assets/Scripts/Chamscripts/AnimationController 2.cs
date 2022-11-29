using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator playerChoiceHandlerAnimation, choiceAnimation;

    [SerializeField]
    public float reactionTime;

    [SerializeField]
    public float maxTime;

    public bool clockisTicking;

    private GameplayController gameplayController;

    public void Start()
    {
        //choiceAnimation.Play("321");
        //choiceAnimation.Play("showchamz");
        //choiceAnimation.Play("321");
        //playerChoiceHandlerAnimation.Play("showholder");
        //choiceAnimation.Play("321");
        clockisTicking = false;
        gameplayController = GetComponent<GameplayController>();
        StartCoroutine(AnimatingStart());

    }

    public IEnumerator AnimatingStart()
    {
        yield return new WaitForSeconds(2f);

        choiceAnimation.Play("showchamz");

        yield return new WaitForSeconds(4f);

        choiceAnimation.Play("showopp");

        yield return new WaitForSeconds(0.2f);

        playerChoiceHandlerAnimation.Play("showholder");

        reactionTime = 0f;

        clockisTicking = true;

        reactionTime = Time.time;
        //playerChoiceHandlerAnimation.Play("win");
    }

    public void ResetAnimations()
    {
        playerChoiceHandlerAnimation.Play("ShowHandler");
        choiceAnimation.Play("RemoveChoices");
    }

    public void PlayerMadeChoice()
    {
        //choiceAnimation.Play("321");
        //show the button
        playerChoiceHandlerAnimation.Play("RemoveHandler");
        //choiceAnimation.Play("ShowChoices");
        choiceAnimation.Play("showplayer");
        choiceAnimation.Play("ExitButton");
        ShowWinner();

        //playerChoiceHandlerAnimation.Play("win");
        print(reactionTime);
        
    }

    public void ShowWinner()
    {
        if (reactionTime < maxTime || gameplayController.Opponent_Choice != gameplayController.player_Choice)
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