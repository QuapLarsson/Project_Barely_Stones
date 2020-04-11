using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum DamageType
    {
        Slashing,
        Blunt,
        Piercing,
        Magical
    }

    public string myName;
    public int myID;
    public int myReach;
    public int myPower;
    public DamageType myDamageType;

    public void MakeSword()
    {
        myName = "Sword";
        myID = 1;
        myReach = 1;
        myPower = 5;
        myDamageType = DamageType.Slashing;
    }
}
