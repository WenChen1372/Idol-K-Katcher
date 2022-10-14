using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Inspector Variables
    [SerializeField]
    [Tooltip("The transform of the player the camera will follow.")]
    private Transform m_PlayerTransform;

    [SerializeField]
    [Tooltip("The offset of the camera from the player origin")]
    private Vector3 m_Offset;

    [SerializeField]
    [Tooltip("The time it will take to reach player (a float in seconds)")]
    private float m_SmoothTime;
    #endregion

    #region Private Variables
    //velocity of camera needed for SmoothDamp
    private Vector3 p_Velocity = Vector3.zero; 
    #endregion 

    #region Updates
    private void LateUpdate()
    {
        Vector3 newPos = m_PlayerTransform.position + m_Offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref p_Velocity, m_SmoothTime);
    }
    #endregion 
}
