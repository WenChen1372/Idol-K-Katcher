using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countdown : MonoBehaviour
{
    [SerializeField]
    public int countdownTime;

    [SerializeField]
    public TextMeshPro display;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    public IEnumerator CountdownToStart()
    {
        
        while (countdownTime > 0)
        {
            display.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        display.text = "GO";
    }
   
}
