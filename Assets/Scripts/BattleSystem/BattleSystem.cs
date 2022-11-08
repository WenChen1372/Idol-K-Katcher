using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//enum with all of the states of our game
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerIdolPrefab;
    public GameObject enemyIdolPrefab;

    public Transform playerIdolSpawn;
    public Transform enemyIdolSpawn;
    //make new battlestate
    public BattleState currState; 

    //Text of the dialogue
    public Text dialogue;


    //The battleHuds
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    //hard coded temporarily for now;
    AIdolClass V_A;
    BIdolClass V_B;

    // Start is called before the first frame update
    void Start()
    {
        
        currState = BattleState.START;
        StartCoroutine(BeginBattle());
        
    }

    IEnumerator BeginBattle()
    {
        GameObject playerIdol = Instantiate(playerIdolPrefab, playerIdolSpawn);
        //might need to change how we get the Idol Classes
        //can use if statements to get Component based off of the tier
        V_A = playerIdol.GetComponent<AIdolClass>();
        Debug.Log(V_A.CurHealth);
        Debug.Log(V_A.IdolName);
        
        
        GameObject enemyIdol = Instantiate(enemyIdolPrefab, enemyIdolSpawn);
        V_B = enemyIdol.GetComponent<BIdolClass>();
        
        //name is currently null

        //having issues with how the get Name works and its not setting to the instance of the object
        dialogue.text = "A sexy " + V_B.IdolNameTest + " challenges you";

        playerHUD.setHUD(V_A);
        enemyHUD.setHUD(V_B);

        yield return new WaitForSeconds(2f);

        currState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogue.text = "Choose a move";
    }

    

    IEnumerator PlayerAttack(int attackNum)
    {
        
          IdolAbility aegyo = V_A.IdolAbilities[attackNum];
          float damage = aegyo.AbilityPower;
          Debug.Log(aegyo.AbilityPower);
          float cost = aegyo.AbilityCost;
          string attackName;

          if(attackNum == 0)
          {
            attackName = "Aegyo";
          } 
          
          else if (attackNum == 1)

          {
            attackName = "Dance";
          }

          else

          {
            attackName = "Sing";
          }

          if (V_A.CurStamina < cost)
          {
            dialogue.text = "You don't have enough stamina";
          }

          else 
          {
            bool isDead = V_B.ChangeHealth(damage);
            enemyHUD.SetHp(V_B.CurHealth);
            Debug.Log(V_B.CurHealth);
            
            playerHUD.SetStamina(cost);
            V_A.ChangeStamina(cost);

            dialogue.text = "You used an " + attackName +  " attack";

            yield return new WaitForSeconds(2f);
            
            Debug.Log(isDead);
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }


          }

    }

    
    
    //EnemyIdolAttack //make AI here
    IEnumerator EnemyTurn()
    {
        dialogue.text = "Enemy V used aegyo";

        yield return new WaitForSeconds(2f);

        IdolAbility aegyo = V_B.IdolAbilities[0];
        float damage = -1*aegyo.AbilityPower;
        float cost = aegyo.AbilityCost;

        bool isDead = V_A.ChangeHealth(damage);
        playerHUD.SetHp(V_A.CurHealth);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            currState = BattleState.LOST;
            EndBattle();
        }

        else 
        {
            currState = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }




    void EndBattle()
    {
        if (currState == BattleState.WON)
        {
            dialogue.text = "You're just better";
        }
        else if (currState == BattleState.LOST)
        {
            dialogue.text = "Get GOOD";
        }
    }

    public void OnAegyoButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack(0));
    }

    public void OnDanceButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack(1));
    }

    public void OnSingButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack(2));
    }




}
