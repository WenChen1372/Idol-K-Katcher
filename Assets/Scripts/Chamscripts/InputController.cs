using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private AnimationController animationController;
    private GameplayController gameplayController;

    private string playersChoice;

    void Awake()
    {
        animationController = GetComponent<AnimationController>();
        gameplayController = GetComponent<GameplayController>();
        gameplayController.SetOpponentChoice();
    }

    public void GetChoice()
    {
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        GameChoices selectedChoice = GameChoices.NONE;

        switch (choiceName)
        {
            case "Up":
                selectedChoice = GameChoices.UP;
                break;

            case "Down":
                selectedChoice = GameChoices.DOWN;
                break;

            case "Left":
                selectedChoice = GameChoices.LEFT;
                break;

            case "Right":
                selectedChoice = GameChoices.RIGHT;
                break;
        }

        gameplayController.SetChoices(selectedChoice);
        animationController.PlayerMadeChoice();
    }


}
