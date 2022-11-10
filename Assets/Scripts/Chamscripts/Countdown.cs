using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countdown : MonoBehaviour
{
    [SerializeField]
    public float reactionTime;

    private bool clockisTicking;

    private void Start()
    {
        reactionTime = 0f;

    }

}
