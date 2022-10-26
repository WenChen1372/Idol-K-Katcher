using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Countdown : MonoBehaviour
{
    public int countdownTime;
    public TMP_Text countdownDisplay;

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "GO!";

    }
}
