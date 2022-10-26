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

    //bool value if camera should follow player or be moved by user
    private bool p_FollowPlayer = true;

    //the first touch a user initiates to move camera
    [SerializeField]
    private Vector3 p_FirstTouch; 
    #endregion

    #region Updates
    //for camera pan
    //for camera zoom
    private void Update()
    {
        //if user touches screen, get that first position
        if (Input.GetMouseButton(0))
        { 
            p_FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //p_FollowPlayer = false;
            //Vector3 moveDirection = p_FirstTouch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += moveDirection;
        }
        //if user continues to HOLD screen (drag), move screen in opposite direction from drag
        
    }

    //for follow player camera
    private void LateUpdate()
    {
        if (p_FollowPlayer)
        {
            Vector3 newPos = m_PlayerTransform.position + m_Offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref p_Velocity, m_SmoothTime);
        }
    }
    #endregion 
}
