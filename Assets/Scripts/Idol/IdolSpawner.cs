using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolSpawner : MonoBehaviour
{
    #region Inspector Variables
    //this should later be changed to a list of Idol class or child of Idol class
    [SerializeField]
    [Tooltip("A list of C tier idols to spawn")]
    private GameObject[] m_IdolsC;

    //this should later be changed to a list of Idol class or child of Idol class
    [SerializeField]
    [Tooltip("A list of B tier idols to spawn")]
    private GameObject[] m_IdolsB;

    //this should later be changed to a list of Idol class or child of Idol class
    [SerializeField]
    [Tooltip("A list of A tier idols to spawn")]
    private GameObject[] m_IdolsA;

    //this should later be changed to a list of Idol class or child of Idol class
    [SerializeField]
    [Tooltip("A list of S tier idols to spawn")]
    private GameObject[] m_IdolsS;

    //this should later be changed to a Tour Stop class object 
    [SerializeField]
    [Tooltip("Tour Stop game object to spawn")]
    private GameObject m_TourStopObject;

    [SerializeField]
    [Tooltip("Battle game object to spawn")]
    private GameObject m_BattleObject; 

    [SerializeField]
    [Tooltip("Player transform in which the spawn location will be based off of")]
    private Transform m_PlayerTransform;

    [SerializeField]
    [Tooltip("Probability that C tier idol will spawn")]
    private float m_ProbabilityC;

    [SerializeField]
    [Tooltip("Probability that B tier idol will spawn")]
    private float m_ProbabilityB; 

    [SerializeField]
    [Tooltip("Probability that A tier idol will spawn")]
    private float m_ProbabilityA;

    [SerializeField]
    [Tooltip("Probability that S tier idol will spawn")]
    private float m_ProbabilityS;

    [SerializeField]
    [Tooltip("Probability that tour stop will spawn")]
    private float m_ProbabilityTourStop;

    [SerializeField]
    [Tooltip("Probability that battle object will spawn")]
    private float m_ProbabilityBattle;

    [SerializeField]
    [Tooltip("Max range of spawn point from player (1 unit = 1 meter")]
    private float m_MinRange;

    [SerializeField]
    [Tooltip("Min range of spawn point from player (1 unit = 1 meter)")]
    private float m_MaxRange;

    //later add min/max wait time
    [SerializeField]
    [Tooltip("Wait time between each idol spawn in seconds")]
    private float m_WaitTime;

    [SerializeField]
    [Tooltip("Wait time between each tour stop spawn in seconds")]
    private float m_WaitTimeTour;

    [SerializeField]
    [Tooltip("Wait time between each battle spawn in seconds")]
    private float m_WaitTimeBattle;

    [SerializeField]
    [Tooltip("The y position of 2D assets on 3D map (same as player, etc.)")]
    private float m_YPos; 
    #endregion

    #region Initialize
    private void Start()
    {
        StartCoroutine(SpawnIdols());
        //StartCoroutine(SpawnTour());
        StartCoroutine(SpawnBattle());

    }
    #endregion

    #region Spawning Idol Methods
    private IEnumerator SpawnIdols()
    {
        while (true)
        {
            InstantiateIdol();
            yield return new WaitForSeconds(m_WaitTime);
        }
    }

    private IEnumerator SpawnTour()
    {
        while (true)
        {
            InstantiateTourStop();
            yield return new WaitForSeconds(m_WaitTimeTour);
        }
    }

    private IEnumerator SpawnBattle()
    {
        while (true)
        {
            InstantiateBattle();
            yield return new WaitForSeconds(m_WaitTimeBattle);
        }
    }

    private void InstantiateIdol()
    {
        //X is left and right
        //Y is depth, will always stay consistent 
        //Z is up and down
        float x = m_PlayerTransform.position.x + RandomCoordinate(); 
        float y = m_YPos;
        float z = m_PlayerTransform.position.z + RandomCoordinate();
        Vector3 position = new Vector3(x, y, z);

        float percent = Random.value;
        int idolNum = Random.Range(0, m_IdolsA.Length-1); 
        if (percent < m_ProbabilityS)
        {
            Instantiate(m_IdolsS[idolNum], position, Quaternion.Euler(90, 0, 0)); 
        } 
        else if (percent < m_ProbabilityS + m_ProbabilityA)
        {
            Instantiate(m_IdolsA[idolNum], position, Quaternion.Euler(90, 0, 0));
        } 
        else if (percent < m_ProbabilityS + m_ProbabilityA + m_ProbabilityB)
        {
            Instantiate(m_IdolsB[idolNum], position, Quaternion.Euler(90, 0, 0));
        } 
        else if (percent < m_ProbabilityS + m_ProbabilityA + m_ProbabilityB + m_ProbabilityC)
        {
            Instantiate(m_IdolsC[idolNum], position, Quaternion.Euler(90, 0, 0));
        } 
        
    }

    private void InstantiateTourStop()
    {
        float x = m_PlayerTransform.position.x + RandomCoordinate();
        float y = m_YPos;
        float z = m_PlayerTransform.position.z + RandomCoordinate();
        Vector3 position2 = new Vector3(x, y, z);

        float percent = Random.value;
        if (percent < m_ProbabilityTourStop)
        {
            Instantiate(m_TourStopObject, position2, Quaternion.Euler(90, 0, 0));
        }
    }

    private void InstantiateBattle()
    {
        float x = m_PlayerTransform.position.x + RandomCoordinate();
        float y = m_YPos;
        float z = m_PlayerTransform.position.z + RandomCoordinate();
        Vector3 position2 = new Vector3(x, y, z);

        float percent = Random.value;
        if (percent < m_ProbabilityBattle)
        {
            Instantiate(m_BattleObject, position2, Quaternion.Euler(90, 0, 0));
        }
    }
    #endregion

    #region Helper Methods
    private float RandomCoordinate()
    {
        float coordinate = Random.Range(m_MinRange, m_MaxRange);
        float posOrNeg = Random.value;
        //50% chance of coordinate being positive or negative
        //just for randomness of spawn location 
        if (posOrNeg <= 0.5)
        {
            return coordinate * -1;
        }
        else
        {
            return coordinate;
        }
    }
    #endregion 
}
