using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Stat Block Start
    public int myExperience = 0;
    public int myLevel = 0;
    public int myPower = 0;
    public int myDefence = 0;
    public int myMovement = 0;
    public int myLuck = 0;
    public int myMaxHP = 0;
    public int myCurrentHP = 0;
    public int myGuts = 0;
    //Stat Block End

    //Equipment Block Start
    public Weapon myWeapon;
    public Armour myArmour;
    //Equipment Block End

    // Start is called before the first frame update
    void Start()
    {
        myWeapon.MakeSword();
        myArmour.MakeLeather();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
