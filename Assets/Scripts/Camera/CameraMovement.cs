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

    //z coordinate of ground
    private float p_GroundZ = 0; 
    #endregion

    #region Updates
    //for camera pan
    //for camera zoom
    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            p_FirstTouch = GetWorldPosition(p_GroundZ);
        } */ 
        /*if (Input.GetMouseButton(0))
        {
            Vector3 direction = p_FirstTouch - GetWorldPosition(p_GroundZ);
            Camera.main.transform.position += direction; 
        }*/
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

    #region World/Plane Methods
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        Vector3 worldPos = mousePos.GetPoint(distance);
        return new Vector3(worldPos.x, Camera.main.transform.position.y, worldPos.y);
    }
    #endregion 
}
