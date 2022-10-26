using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerChoiceHandlerAnimation, choiceAnimation;

    public void ShowOpponent()
    {
        choiceAnimation.Play("showchamz");
    }
    public void Countdown()
    {
        //countdown animation
    }
    public void ResetAnimations()
    {
        playerChoiceHandlerAnimation.Play("ShowHandler");
        choiceAnimation.Play("RemoveChoices");
    }

    public void PlayerMadeChoice()
    {
        //show the button
        playerChoiceHandlerAnimation.Play("RemoveHandler");
        //choiceAnimation.Play("ShowChoices");
        //choiceAnimation.Play("showchamz");
        choiceAnimation.Play("ExitButton");
    }

}