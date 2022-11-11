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
    public EnemyBattleHUD enemyHUD;

    //The idol prefabs components
    IdolClass playerIdolComp;
    IdolClass enemyIdolComp;

    // Start is called before the first frame update
    void Start()
    {
        
        currState = BattleState.START;
        StartCoroutine(BeginBattle());
        
    }

    //Set our player GameObject
    public void setPlayerIdol(GameObject ourIdol)
    {
        playerIdolPrefab = ourIdol;
    }

    //Set out enemy GameObject
    public void setEnemyIdol(GameObject enemyIdol)
    {
        enemyIdolPrefab = enemyIdol;
    }

    IEnumerator BeginBattle()
    {
        GameObject playerIdol = Instantiate(playerIdolPrefab, playerIdolSpawn);

        Debug.Log(playerIdol == null);
        //might need to change how we get the Idol Classes
        //can use if statements to get Component based off of the tier
        playerIdolComp = playerIdol.GetComponent<IdolClass>();
        Debug.Log(playerIdolComp.CurHealth);
        Debug.Log(playerIdolComp.getIdolName());
        
        
        GameObject enemyIdol = Instantiate(enemyIdolPrefab, enemyIdolSpawn);
        enemyIdolComp = enemyIdol.GetComponent<IdolClass>();
        Debug.Log(enemyIdolComp.getIdolName());
        //name is currently null

        //having issues with how the get Name works and its not setting to the instance of the object
        
        dialogue.text = "A sexy " + enemyIdolComp.getIdolName() + " challenges you";

        playerHUD.setHUD(playerIdolComp);
        enemyHUD.setHUD(enemyIdolComp);

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
        
          IdolAbility attack = playerIdolComp.IdolAbilities[attackNum];
          float damage = attack.AbilityPower;
          Debug.Log(attack.AbilityPower);
          float cost = attack.AbilityCost;
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

          if (playerIdolComp.CurStamina < cost)
          {
            dialogue.text = "You don't have enough stamina";
          }

          else 
          {
            bool isDead = enemyIdolComp.ChangeHealth(damage);
            enemyHUD.SetHp(enemyIdolComp.CurHealth);
            Debug.Log(enemyIdolComp.CurHealth);
            
            playerHUD.SetStamina(cost);
            playerIdolComp.ChangeStamina(cost);

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

        IdolAbility attack = enemyIdolComp.IdolAbilities[0];
        float damage = -1*attack.AbilityPower;
        float cost = attack.AbilityCost;

        bool isDead = playerIdolComp.ChangeHealth(damage);
        playerHUD.SetHp(playerIdolComp.CurHealth);

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

    public void OnSpecialButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack(3));
    }






}
