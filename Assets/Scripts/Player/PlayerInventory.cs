using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{


    //public Dictionary<IdolClass, int> IdolCollection = new Dictionary<IdolClass, int>();
    public Dictionary<string, Dictionary<char, IdolClass>> IdolCollection = new Dictionary<string, Dictionary<char, IdolClass>>();




    //Get all of the Idols the player currently has
    public Dictionary<string, Dictionary<char, IdolClass>> GetIdolCollection() {
        return IdolCollection;
    }

    //addIdol to our collection
    public void addIdol(IdolClass Idol)
    {
        string name = Idol.IdolName;
        char tier = Idol.IdolTier;

        if (IdolCollection.ContainsKey(name))
        {

            Dictionary<char, IdolClass> tierStorage = IdolCollection[name];

            if (tierStorage.ContainsKey(tier))
            {

                IdolClass getIdol = tierStorage[tier]; //get the idol
                getIdol.Count = getIdol.Count += 1; //increase the count of the Idol
                tierStorage[tier] = getIdol; //add the IdolClass back to the tierStorage

            }

            else

            {
                Idol.Count = Idol.Count + 1; //increase the Idol count
                tierStorage.Add(tier, Idol); //add it to our storagee
                
            }
        }
       
        else
       
        {
            Dictionary<char, IdolClass> tierStorage = new Dictionary<char, IdolClass>(); //make new Dic for the tier since we don't have the Idol
            Idol.Count = Idol.Count + 1;
            tierStorage.Add(tier, Idol);
            IdolCollection.Add(name, tierStorage); //add our Idol to our Colleciton
        }
    }

    //retun the list of all IdolGameObjects
    public List<IdolClass> allIdols()
    {
        List<IdolClass> all = new List<IdolClass>();
        foreach(KeyValuePair<string, Dictionary<char, IdolClass>> entry in IdolCollection)
        {
            Dictionary<char, IdolClass> tierIdol = entry.Value;
            
            foreach(KeyValuePair<char, IdolClass> item in tierIdol)
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
