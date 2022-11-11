using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{


    //public Dictionary<IdolClass, int> IdolCollection = new Dictionary<IdolClass, int>();
    public Dictionary<string, Dictionary<char, GameObject>> IdolCollection = new Dictionary<string, Dictionary<char, GameObject>>();



    

    //Get all of the Idols the player currently has
    public Dictionary<string, Dictionary<char, GameObject>> GetIdolCollection() {
        return IdolCollection;
    }

    //addIdol to our collection
    public void addIdol(GameObject Idol)

    {   
        IdolClass getIdol = Idol.GetComponent<IdolClass>();
        string name = getIdol.getIdolName();
        char tier = getIdol.IdolTier;


        if (IdolCollection.ContainsKey(name))
        {

            Dictionary<char, GameObject> tierStorage = IdolCollection[name];

            if (tierStorage.ContainsKey(tier))
            {




                GameObject getIdolStore = tierStorage[tier]; //get the idol
                IdolClass getIdolComp = getIdolStore.GetComponent<IdolClass>();
                getIdolComp.Count = getIdolComp.Count += 1; //increase the count of the Idol
                tierStorage[tier] = getIdolStore; //add the IdolClass back to the tierStorage


            }

            else

            {



                getIdol.Count = getIdol.Count + 1; //increase the Idol count

                
            }
        }
       
        else
       
        {
            Dictionary<char, GameObject> tierStorage = new Dictionary<char, GameObject>(); //make new Dic for the tier since we don't have the Idol

            getIdol.Count = getIdol.Count + 1;
            tierStorage.Add(tier, Idol);
            IdolCollection.Add(name, tierStorage); //add our Idol to our Colleciton
        }
    }

    //retun the list of all IdolGameObjects
    public List<GameObject> allIdols()
    {
        List<GameObject> all = new List<GameObject>();
        foreach(KeyValuePair<string, Dictionary<char, GameObject>> entry in IdolCollection)
        {
            Dictionary<char, GameObject> tierIdol = entry.Value;
            
            foreach(KeyValuePair<char, GameObject> item in tierIdol)
            {
                all.Add(item.Value);
            }
        }

        return all; 
    }


    //how many contracts the player has for upgrades
    private int contracts;
    public int Contracts
    {
        get;
        set;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
