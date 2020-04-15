using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmourType
{
    Heavy,
    Medium,
    Light,
    Magical
}

public class Armour : MonoBehaviour
{
    public string myName;
    public int myID;
    public int myDefence;
    public ArmourType myArmourType;

    public void MakeLeather()
    {
        myName = "Leather Armour";
        myID = 1;
        myDefence = 5;
        myArmourType = ArmourType.Medium;
    }
}
