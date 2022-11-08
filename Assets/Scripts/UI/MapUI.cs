using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class SampleSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void GoBackToMap()
    {
        SceneManager.LoadScene("Location-basedGame"); 
    }
}
