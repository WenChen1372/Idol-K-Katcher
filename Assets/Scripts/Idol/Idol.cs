using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Idol : MonoBehaviour
{
    public string Name = "None";
    public string Tier = "None";


}

class V_C_tier : Idol
{
    public string Name = "V";
    public string Tier = "C";

        //have code here that loads the png of the character sprite
}

class V_B_tier : Idol
{
    public string Name = "V";
    public string Tier = "B";

    //have code here that loads the png of the character sprite
}

class V_A_tier : Idol
{
    public string Name = "V";
    public string Tier = "A";

    //have code here that loads the png of the character sprite
}

class V_S_tier : Idol
{
    public string Name = "V";
    public string Tier = "S";

    //have code here that loads the png of the character sprite
}
