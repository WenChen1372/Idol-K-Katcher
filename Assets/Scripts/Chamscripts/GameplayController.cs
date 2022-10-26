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

    [SerializeField]
    private Sprite up_Sprite, down_Sprite, right_Sprite, left_Sprite;

    [SerializeField]
    private Image playerChoice_Img, oponnentChoice_Img;

    [SerializeField]
    private TMP_Text infoText;

    [SerializeField]
    private int countdownTime;

    private TMP_Text countdownDisplay;

    private GameChoices player_Choice = GameChoices.NONE, Opponent_Choice = GameChoices.NONE;

    private AnimationController animationController;

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)

        {
            print("hi");
            countdownDisplay.text = countdownTime.ToString();
            print(countdownTime);

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "GO!";
    }
    void Awake()
    {
        animationController = GetComponent<AnimationController>();
        StartCoroutine(CountdownToStart());
        print("hi!");
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
        SetOpponentChoice();

        DetermineWinner();
    }
    void SetOpponentChoice()
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
    void DetermineWinner()
    {
        if (player_Choice == Opponent_Choice)
        {
            //infoText.text = "LOSER ";
            StartCoroutine(DisplayWinnerAndRestart());

            return;
        }

        if (player_Choice != Opponent_Choice)
        {
            //infoText.text = "WINNER";

            StartCoroutine(DisplayWinnerAndRestart());

            return;
        }
        

    }

    IEnumerator DisplayWinnerAndRestart()
    {
        yield return new WaitForSeconds(2f);

        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        infoText.gameObject.SetActive(false);

        animationController.ResetAnimations();
    }
}
