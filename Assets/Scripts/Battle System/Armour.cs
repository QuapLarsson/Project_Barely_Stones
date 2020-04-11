using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{
    public enum ArmourType
    {
        Heavy,
        Medium,
        Light,
        Magical
    }

    public string myName;
    public int myID;
    public int myDefenceBonus;
    public ArmourType myArmourType;

    public void MakeLeather()
    {
        myName = "Leather Armour";
        myID = 1;
        myDefenceBonus = 5;
        myArmourType = ArmourType.Medium;
    }
}
