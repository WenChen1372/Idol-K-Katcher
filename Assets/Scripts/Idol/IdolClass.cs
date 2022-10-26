using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdolClass : MonoBehaviour
{
    #region Inspector Variables

    #endregion

    [SerializeField]
    [Tooltip("The health of the idol")]
    private float health;
    public float Health
    {
        get;
    }

    [SerializeField]
    [Tooltip("The stamina of the idol")]
    private float stamina;
    public float Stamina
    {
        get;
    }

    [SerializeField]
    [Tooltip("The 2D sprite model of the idol photocard")]
    private GameObject idolPhotoCard;
    public GameObject IdolPhotoCard
    {
        get;
    }

    [SerializeField]
    [Tooltip("A list of abilities of the idol.")]
    private IdolAbility[] idolAttacks;
    public IdolAbility[] IdolAttacks;
}
