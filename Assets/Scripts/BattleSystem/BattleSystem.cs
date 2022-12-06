using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using TMPro;
using UnityEngine.SceneManagement; 

//enum with all of the states of our game
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour, IDataPersistance
{

    #region Variables

    public GameObject playerIdolPrefab;
    //object im saving
    //*********************************
    public GameObject enemyIdolPrefab;

    public Transform playerIdolSpawn;
    public Transform enemyIdolSpawn;
    //make new battlestate
    public BattleState currState; 

    //Text of the dialogue
    public TextMeshProUGUI dialogue;


    //The battleHuds
    public BattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    //The idol prefabs components
    IdolClass playerIdolComp;
    IdolClass enemyIdolComp;

    //the float for the base regen
    float ourBaseRegen;
    float enemyBaseRegen;

    //the aegyo drain percentage
    float aegyoDrain;
    float enemyAegyoDrain;

    //the boolean for if you used the aegyo move
    bool usedAegyo;
    bool enemyUsedAegyo;

    //the name of the idols
    string ourIdolName;
    string enemyIdolName;

    //ourIdolTier
    char ourIdolTier;
    char enemyIdolTier;

    //if you are poisoned
    bool ourIdolPoison;
    bool enemyIdolPoison;

    bool usedSpecial;
    bool enemyUsedSpecial;

    //our idol gameobjects
    GameObject playerIdol;
    GameObject enemyIdol;

    //temp idol xp variable
    private int tempXP;

    //temp idol training points variable
    private int tempTrainingPoints;

    //temp idol tier variable
    private char tempTier;

    //temp idol name variable
    private string tempName;

    //temp prefabIdol dictionary
    private Dictionary<string, GameObject> tempPrefabIdol;

    //temp inventoryCount dictionary 
    private Dictionary<string, int> tempInventoryCount; 

    #endregion

    #region IDataPersistance Methods
    //in implementing script, just assign variables you want to data.(variable) value
    public void LoadData(GameData data)
    {
        Debug.Log(data.playerSelection);
        enemyIdolPrefab = data.playerCurIdol;
        playerIdolPrefab = data.playerSelection; 
        tempTrainingPoints = data.playerTrainingPoints;
        tempXP = data.playerXP;
        tempTier = data.playerCurTier;
        tempName = data.playerCurName;
        tempPrefabIdol = data.playerPrefabIdols;
        tempInventoryCount = data.playerInventoryCount; 
    }

    //in implementing script, just assign data.(variable) to variable value you want 
    public void SaveData(GameData data)
    {
        data.playerXP = tempXP;
        data.playerTrainingPoints = tempTrainingPoints;
        data.playerCurIdol = enemyIdolPrefab;
        data.playerCurTier = tempTier;
        data.playerCurName = tempName;
        data.playerPrefabIdols = tempPrefabIdol;
        data.playerInventoryCount = tempInventoryCount;
        data.playerSelection = playerIdolPrefab; 
    }
    #endregion 
    #region begin
    // Start is called before the first frame update
    void Start()
    {

        currState = BattleState.START;
        usedAegyo = false;
        enemyUsedAegyo = false;
        ourIdolPoison = false;
        enemyIdolPoison = false;
        usedSpecial = false;
        enemyUsedSpecial = false;
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
        
        playerIdol = Instantiate(playerIdolPrefab, new Vector3(-1.1f, 1f, 0f), Quaternion.identity);

        Debug.Log(playerIdol == null);
        //might need to change how we get the Idol Classes
        //can use if statements to get Component based off of the tier
        playerIdolComp = playerIdol.GetComponent<IdolClass>();
        IdolAbility attack = playerIdolComp.getIdolAbility()[2];
        float damage = attack.getAbilityPower();
        
        
        enemyIdol = Instantiate(enemyIdolPrefab, new Vector3(1.1f, 1f, 0f), Quaternion.identity);
        enemyIdolComp = enemyIdol.GetComponent<IdolClass>();
    
        //name is currently null

        //having issues with how the get Name works and its not setting to the instance of the object

        //set idolName
        ourIdolName = playerIdolComp.getIdolName();
        enemyIdolName = enemyIdolComp.getIdolName();
        
        dialogue.text = "A sexy " + enemyIdolComp.getIdolName() + " challenges you";

        ourIdolTier = playerIdolComp.IdolTier;
        enemyIdolTier = enemyIdolComp.IdolTier;

        ourBaseRegen = setRegen(ourIdolTier);
        enemyBaseRegen = setRegen(enemyIdolTier);

        aegyoDrain = setAegyoDrain(ourIdolTier);
        enemyAegyoDrain = setAegyoDrain(enemyIdolTier);

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

    #endregion

    

    IEnumerator PlayerAttack(int attackNum)
    {
        
          IdolAbility attack = playerIdolComp.getIdolAbility()[attackNum];
          float damage = attack.getAbilityPower();
          float cost = attack.getAbilityCost();
          string attackName;

          if(attackNum == 0 && !usedAegyo)
          {
            attackName = "Aegyo";
            usedAegyo = true;

          }

          else if (attackNum == 0 && usedAegyo)
          {

            dialogue.text = "You can only use Aegyo once per Battle";

            yield return new WaitForSeconds(2f);

            dialogue.text = "Choose a move";

            yield return new WaitForSeconds(2f);

            yield break;


          }
          
          else if (attackNum == 1)

          {
            attackName = "Sing";
          }

          else 

          {
            attackName = "Dance";
          }

          if (playerIdolComp.CurStamina < cost)
          {
            dialogue.text = "You don't have enough stamina";

            yield return new WaitForSeconds(2f);
            
            dialogue.text = "Choose a move";

            yield return new WaitForSeconds(2f);

            yield break;

        }

          else 
          {

            StartCoroutine(movePlayerAttack());

            bool isDead = enemyIdolComp.ChangeHealth(damage);
            bool weDead = false;
            enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
            Debug.Log(enemyIdolComp.CurHealth);
            
        
            playerIdolComp.ChangeStamina(cost);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            dialogue.text = "You used an " + attackName +  " attack";

            yield return new WaitForSeconds(2f);



            if (attackNum == 0)
            {
                float percentage = enemyIdolComp.CurStamina * aegyoDrain; //not sure from current stamina or max stamina?

                enemyIdolComp.ChangeStamina(percentage);
                enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

                dialogue.text = "Enemy was drained for " + percentage.ToString() + " stamina";
                
                yield return new WaitForSeconds(2f);

            }

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                StartCoroutine(EnemyTurn());
            }


          }

    }


    //EnemyIdolAttack //make AI here
    IEnumerator EnemyTurn()
    {

        yield return new WaitForSeconds(2f);

        string attackName = "";
        int attackNum;
        if (!enemyUsedAegyo && !enemyUsedSpecial)
        {
            Random number = new Random();
            attackNum = number.Next(0, 4);

            if (attackNum == 0)
            {
                enemyUsedAegyo = true;

            }

            if (attackNum == 3)
            {
                enemyUsedSpecial = true;
            }
        } 
        
        else if (!enemyUsedSpecial && enemyUsedAegyo)
        {
            Random number = new Random();
            attackNum = number.Next(1, 4);
            enemyUsedSpecial = true;
            if (attackNum == 3)
            {
                enemyUsedSpecial = true;
            }
        }

        else if (enemyUsedSpecial && !enemyUsedAegyo)
        {
            Random number = new Random();
            attackNum = number.Next(0, 3);
            enemyUsedSpecial = true;
            if (attackNum == 0)
            {
                enemyUsedAegyo = true;
            }
        }

        else
        {
            Random number = new Random();
            attackNum = number.Next(1, 3); 

        }
        
        if(attackNum == 0)
        {

            enemyUsedAegyo = true;
            attackName = "Aegyo";
        } 
          
        else if (attackNum == 1)

        {
            attackName = "Sing";
        }

        else if (attackNum == 2)

        {
            attackName = "Dance";
        }

        else
        {
            StartCoroutine(enemySpecialMove(enemyIdolName));

            yield break;
        }

        StartCoroutine(moveEnemyPlayerAttack());



        IdolAbility attack = enemyIdolComp.getIdolAbility()[attackNum];
        float damage = attack.getAbilityPower();
        float cost = attack.getAbilityCost();
        Debug.Log(damage);
        bool isDead = playerIdolComp.ChangeHealth(damage);
        bool weDead = false;
        playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);


        enemyIdolComp.ChangeStamina(cost);
        enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

        dialogue.text = "Enemy " + enemyIdolComp.getIdolName().ToString() + " used "  + attackName;

        if (attackNum == 0)
        {
            float percentage = playerIdolComp.CurStamina * enemyAegyoDrain; //not sure from current stamina or max stamina?

            playerIdolComp.ChangeStamina(percentage);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            dialogue.text = "Your idol was drained " + percentage.ToString() + " stamina";
                
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(2f);

        if (ourIdolPoison)
        {
            isDead = wePoison();

            dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

            yield return new WaitForSeconds(2f);
        }

        if (enemyIdolPoison)
        {
            weDead = enemyPoison();

            dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

            yield return new WaitForSeconds(2f);
        }

        if (isDead)
        {
            currState = BattleState.LOST;
            EndBattle();
        }

        else 
        {
            currState = BattleState.PLAYERTURN;
            
            IdolRegen();

            if (!ourIdolPoison)
            {
                dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);

            }

            if (!enemyIdolPoison)
            {

                dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);

            }

            PlayerTurn();
        }

    }

    #region regen

    //Regen the health and the Stamina
    public void IdolRegen()
    {

        if (!ourIdolPoison)
        {
            playerIdolComp.RegenHealth(ourBaseRegen);
            playerIdolComp.RegenStamina(ourBaseRegen);

            playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

        }

        if (!enemyIdolPoison)
        {
            enemyIdolComp.RegenHealth(enemyBaseRegen);
            enemyIdolComp.RegenStamina(enemyBaseRegen);

            enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
            enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

        }

     }

     

    public bool wePoison()
    {
        bool isDead = playerIdolComp.ChangeHealth(enemyBaseRegen);
        playerIdolComp.ChangeStamina(enemyBaseRegen);

        playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);
        playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

        return isDead;


    }

    public bool enemyPoison()
    {
        bool isDead = enemyIdolComp.ChangeHealth(ourBaseRegen);
        enemyIdolComp.ChangeStamina(enemyBaseRegen);

        enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
        enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

        return isDead;

    }

    #endregion 


    #region movement

    IEnumerator movePlayerAttack()
    {
        playerIdol.transform.position = new Vector3(-0.5f, 1f, 0f);

        enemyIdol.transform.position = new Vector3(1.3f, 1f, 0);

        yield return new WaitForSeconds(1f);

        playerIdol.transform.position = new Vector3(-0.6f, 1f, 0f);


        yield return new WaitForSeconds(0.75f);

        playerIdol.transform.position = new Vector3(-0.5f, 1f, 0f);

        yield return new WaitForSeconds(0.75f);

        playerIdol.transform.position = new Vector3(-1.1f, 1f, 0f);

        enemyIdol.transform.position = new Vector3(1.1f, 1f, 0);

        yield break;

    }

    IEnumerator moveEnemyPlayerAttack()
    {
        enemyIdol.transform.position = new Vector3(0.6f, 1f, 0f);

        playerIdol.transform.position = new Vector3(-1.3f, 1f, 0);

        yield return new WaitForSeconds(1f);

        enemyIdol.transform.position = new Vector3(0.7f, 1f, 0f);


        yield return new WaitForSeconds(0.75f);

        enemyIdol.transform.position = new Vector3(0.6f, 1f, 0f);

        yield return new WaitForSeconds(0.75f);

        playerIdol.transform.position = new Vector3(-1.1f, 1f, 0f);

        enemyIdol.transform.position = new Vector3(1.1f, 1f, 0);

        yield break;

    }

    #endregion

    void EndBattle()
    {
        if (currState == BattleState.WON)
        {
            dialogue.text = "You're just better";
            //add xp and training
            if (tempTier == 'S')
            {
                tempTrainingPoints += 4000;
                tempXP += 400; 
            } 
            else if (tempTier == 'A')
            {
                tempTrainingPoints += 3000;
                tempXP += 300;
            } 
            else if (tempTier == 'B')
            {
                tempTrainingPoints += 2000;
                tempXP += 200;
            } 
            else
            {
                tempTrainingPoints += 1000;
                tempXP += 100;
            }
            
            SceneManager.LoadSceneAsync("Battle_Win");
            //load winning scene
        }
        else if (currState == BattleState.LOST)
        {
            dialogue.text = "Get GOOD";
            //load scene

            SceneManager.LoadSceneAsync("Battle_Loss");
        }
    }

    #region attackButtons

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

        StartCoroutine(PlayerAttack(2));
    }

    public void OnSingButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack(1));
    }

    public void OnSpecialButton()
    {
        if (currState != BattleState.PLAYERTURN)
        {
            return;
        }

        
        StartCoroutine(SpecialMove(ourIdolName));
            
        
    }

    #endregion

    #region setregn
    private float setRegen(char Tier)
    {

        float[] values = {5f, 10f, 15f, 20f}; 

        if (Tier == 'C')
        {
            return values[0]; 
        }

        else if (Tier == 'B')
        {
            return values[1];
        }

        else if (Tier == 'A')
        {
            return values[2];
        }

        else
        {
            return values[3];
        }
    }

    private float setAegyoDrain(char Tier)
    {
        float[] values = {0.10f, 0.20f, 0.30f, 0.40f};
        
        if (Tier == 'C')
        {
            return values[0]; 
        }

        else if (Tier == 'B')
        {
            return values[1];
        }

        else if (Tier == 'A')
        {
            return values[2];
        }

        else
        {
            return values[3];
        }

    }
    #endregion

    #region ourSpecialMove
    IEnumerator SpecialMove(string idolName)
    {

        if (usedSpecial)
        {
            dialogue.text = "You can only use special once";

            yield return new WaitForSeconds(3f);

            PlayerTurn();

            yield break;
        }

        usedSpecial = true;

        if (idolName == "Jhope")
        {
            dialogue.text = "As one of the most positive members of BTS";

            yield return new WaitForSeconds(2f);

            dialogue.text = "His bright personality blinds the foe and makes them skip a turn";

            yield return new WaitForSeconds(2f);

            float percentage = enemyIdolComp.CurStamina * aegyoDrain; //not sure from current stamina or max stamina?

            enemyIdolComp.ChangeStamina(percentage);
            enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

            dialogue.text = "Enemy was drained for " + percentage.ToString() + " stamina";
                
            yield return new WaitForSeconds(2f);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

            currState = BattleState.PLAYERTURN;


            PlayerTurn();

        }

        else if (idolName == "Rm")
        {
            dialogue.text = "Rm raps so fast, he boosts his health and stamina";

            yield return new WaitForSeconds(2f);

            dialogue.text = "FOR THE REST OF THE BATTLE";

            yield return new WaitForSeconds(2f);

            ourBaseRegen = ourBaseRegen * 2;

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }


            currState = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());
        }

        else if (idolName == "Suga")
        {
            dialogue.text = "Suga's bluntness and honesty is something that could scare brave hearts";

            yield return new WaitForSeconds(2f);

            dialogue.text = "Face the deadly stare of Suga and defeat as this ability gives you a percent chance";

            yield return new WaitForSeconds(2f);

            dialogue.text = "TO WIN THE GAME OR LOSE";

            bool win = SugaSpecial(ourIdolTier);

            if (win)
            {

                dialogue.text = "YOU GOT LUCKY AND WON";

                yield return new WaitForSeconds(2f);

                currState = BattleState.WON;

                EndBattle();
            }

            else
            {

                dialogue.text = "YOU GOT UNLUCKY AND LOST ";

                yield return new WaitForSeconds(2f);

                currState = BattleState.LOST;

                EndBattle();
            }
            
        }

        else if (idolName == "Jk")

        {
            dialogue.text = "The maknae of BTS uses his youth to restore his health to max health";

            yield return new WaitForSeconds(3f);

            playerIdolComp.ResetHealth();
            playerIdolComp.ResetStamina();

            playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

    

            currState = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());
        }

        else if (idolName == "Jin")

        {
            dialogue.text = "Jin can be super witty and corny at times with his cringe dad jokes";

            yield return new WaitForSeconds(2f);

            dialogue.text = "So as the joker of the group, he swutches stats with his opponent";

            yield return new WaitForSeconds(2f);

            JinSpecial();

            playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
            enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

        

            currState = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());


        } 
        
        else if (idolName == "Jimin")

        {
            dialogue.text = "As one of the best looking members, his looks charm his opponents";

            yield return new WaitForSeconds(2f);

            dialogue.text = "making them give give their special ability to Jimin";

            yield return new WaitForSeconds(2f);

            dialogue.text = "Jimin is now " + enemyIdolName.ToString() + " and uses his ability";

            yield return new WaitForSeconds(2f);


            StartCoroutine(SpecialMove(enemyIdolName));
        }

        else if (idolName == "V")

        {
            dialogue.text = "Being part of so many dating scandals";

            yield return new WaitForSeconds(2f);

            dialogue.text = "V uses the toxic publicity to poison his enemy by reducing health and stamina";

            yield return new WaitForSeconds(2f);

            dialogue.text = "For the rest of the battle";

            yield return new WaitForSeconds(1f);

            enemyIdolPoison = true;
            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

            currState = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());





        }



    }

    #endregion

    #region enemySpecial
    IEnumerator enemySpecialMove(string idolName)
    {

        
        if (idolName == "Jhope")
        {
            dialogue.text = "As one of the most positive members of BTS";

            yield return new WaitForSeconds(3f);

            dialogue.text = "His bright personality blinds the foe and makes them skip a turn";

            yield return new WaitForSeconds(3f);

            float percentage = playerIdolComp.CurStamina * enemyAegyoDrain; //not sure from current stamina or max stamina?

            playerIdolComp.ChangeStamina(percentage);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            dialogue.text = "You were drained for " + percentage.ToString() + " stamina";
                
            yield return new WaitForSeconds(2f);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

            currState = BattleState.ENEMYTURN;


            StartCoroutine(EnemyTurn());

            yield break; 

        }

        else if (idolName == "Rm")
        {
            dialogue.text = "Rm raps so fast, he boosts his health and stamina";

            yield return new WaitForSeconds(2f);

            dialogue.text = "FOR THE REST OF THE BATTLE";

            yield return new WaitForSeconds(2f);

            enemyBaseRegen = enemyBaseRegen * 2;

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }


            currState = BattleState.PLAYERTURN;

            PlayerTurn();

            yield break;
        }

        else if (idolName == "Suga")
        {
            dialogue.text = "Suga's bluntness and honesty is something that could scare brave hearts";

            yield return new WaitForSeconds(2f);

            dialogue.text = "Face the deadly stare of Suga and defeat as this ability gives you a percent chance";

            yield return new WaitForSeconds(2f);

            dialogue.text = "TO WIN THE GAME OR LOSE";

            bool win = SugaSpecial(enemyIdolTier);

            if (win)
            {

                dialogue.text = "ENEMY SUGA GOT LUCKY AND WON";

                yield return new WaitForSeconds(2f);

                currState = BattleState.LOST;

                EndBattle();
            }

            else
            {

                dialogue.text = "ENEMY SUGA GOT UNLUCKY AND LOST ";

                yield return new WaitForSeconds(2f);

                currState = BattleState.WON;

                EndBattle();
            }
            
        }

        else if (idolName == "Jk")

        {
            dialogue.text = "The maknae of BTS uses his youth to restore his health to max health";

            yield return new WaitForSeconds(3f);

            enemyIdolComp.ResetHealth();
            enemyIdolComp.ResetStamina();

            enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
            enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(3f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(3f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

    

            currState = BattleState.PLAYERTURN;

            PlayerTurn();

            yield break;
        }

        else if (idolName == "Jin")

        {
            dialogue.text = "Jin can be super witty and corny at times with his cringe dad jokes";

            yield return new WaitForSeconds(2f);

            dialogue.text = "So as the joker of the group, he swutches stats with his opponent";

            yield return new WaitForSeconds(2f);

            JinSpecial();

            playerHUD.SetHp(playerIdolComp.CurHealth, playerIdolComp);
            playerHUD.SetStamina(playerIdolComp.CurStamina, playerIdolComp);

            enemyHUD.SetHp(enemyIdolComp.CurHealth, enemyIdolComp);
            enemyHUD.SetStamina(enemyIdolComp.CurStamina, enemyIdolComp);

            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

        

            currState = BattleState.PLAYERTURN;

            PlayerTurn();

            yield break;


        } 
        
        else if (idolName == "Jimin")

        {
            dialogue.text = "As one of the best looking members, his looks charm his opponents";

            yield return new WaitForSeconds(2f);

            dialogue.text = "making them give give their special ability to Jimin";

            yield return new WaitForSeconds(2f);

            dialogue.text = "Jimin is now " + enemyIdolName.ToString() + " and uses his ability";

            yield return new WaitForSeconds(2f);


            StartCoroutine(enemySpecialMove(ourIdolName));

        }

        else if (idolName == "V")

        {
            dialogue.text = "Being part of so many dating scandals";

            yield return new WaitForSeconds(2f);

            dialogue.text = "V uses the toxic publicity to poison his enemy by reducing health and stamina";

            yield return new WaitForSeconds(2f);

            dialogue.text = "For the rest of the battle";

            yield return new WaitForSeconds(1f);

            ourIdolPoison = true;
            bool weDead = false;
            bool isDead = false;

            if (ourIdolPoison)
            {
                weDead = wePoison();

                dialogue.text = "You were poisoned " + enemyBaseRegen.ToString() + " Health and " + enemyBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            if (enemyIdolPoison)
            {
                isDead = enemyPoison();

                dialogue.text = "Enemy was poisoned " + ourBaseRegen.ToString() + " Health and " + ourBaseRegen.ToString() + " Stamina";

                yield return new WaitForSeconds(2f);
            }

            
            
            if (isDead)
            {
                currState = BattleState.WON;
                EndBattle();
            }

            else if (weDead)
            {
                currState = BattleState.LOST;
                EndBattle();
            }
            else
            {
                currState = BattleState.ENEMYTURN;

                IdolRegen();

                if (!ourIdolPoison)
                {
                    dialogue.text = "You healed " + ourBaseRegen.ToString() + " Health and regained " + ourBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                if (!enemyIdolPoison)
                {

                    dialogue.text = "Enemy healed " + enemyBaseRegen.ToString() + " Health and regained " + enemyBaseRegen.ToString() + " Stamina";

                    yield return new WaitForSeconds(2f);

                }

                
            }

            currState = BattleState.PLAYERTURN;

            PlayerTurn();

            yield break;





        }



    }


    #endregion

    #region Suga

    public bool SugaSpecial(char idolTier)
    {
        Random number = new Random();
        int Lucky = number.Next(0, 101);

        if (idolTier == 'C')
        {
            if (Lucky <= 10)
            {
                return true;
            }

        }

        else if (idolTier == 'B')
        {
            if (Lucky <= 20)
            {
                return true;
            }

        }

        else if (idolTier == 'A')
        {
            if (Lucky <= 30)
            {
                return true;
            }

        }

        else if (idolTier == 'S')
        {
            if (Lucky <= 40)
            {
                return true;
            }

        }

        return false;

    }

    #endregion

    #region Jin Special

    public void JinSpecial()
    {
        float ourCurHealth = playerIdolComp.CurHealth;
        float ourCurStamina = playerIdolComp.CurStamina;
        float ourMaxHealth = playerIdolComp.Health;
        float ourMaxStamina = playerIdolComp.Stamina;

        float enemyCurHealth = enemyIdolComp.CurHealth;
        float enemyCurstamina = enemyIdolComp.CurStamina;
        float enemyMaxHealth = enemyIdolComp.Health;
        float enemyMaxStamina = enemyIdolComp.Stamina;

        playerIdolComp.SwapHealth(enemyCurHealth, enemyMaxHealth);


        enemyIdolComp.SwapHealth(ourCurHealth, ourMaxHealth);
    

    }

    #endregion


}
