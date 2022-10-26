using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    #region Trigger Methods
    void OnTriggerStay(Collider other)
    {
        //Only accounts for 1 touch
        //Will not consider multiple touches
        if (other.CompareTag("Idol"))
        {
            //Only accounts for 1 touch
            //Will not consider multiple touches
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (other.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    SceneManager.LoadScene("Cham");
                }
            }
        }
    }
    #endregion 
}