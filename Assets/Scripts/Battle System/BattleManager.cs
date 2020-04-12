using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Fighter myFighter0;
    public Fighter myFighter1;

    bool myActiveFighter; //False = 0, True = 1

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Take in two fighters and start the battle. Entry point for battles.
    public void Init(Fighter aFighter0, Fighter aFighter1)
    {
        myActiveFighter = false;
        myFighter0 = aFighter0;
        myFighter1 = aFighter1;

        //TODO: Shift camera and present battle scene

        StartFight(myFighter0, myFighter1);
    }

    //Start battle sequence. Kapow.
    void StartFight(Fighter aFighter0, Fighter aFighter1)
    {
        //myFighter0 hits opponent and switch active fighter
        //myFighter1 hits opponent and switch active fighter
        //Reset scene to strategy mode
    }

    //Active makes attack on inactive Fighter, then active fighter is switched
    void MakeAttack(Fighter aActiveFighter, Fighter aInactiveFighter)
    {
        float damageRate = 1f;
        int attackerTotalPower;
        int defenderTotalDefence;

        if (myActiveFighter == false) //Active fighter = 0
        {
            attackerTotalPower = aActiveFighter.myPower + aActiveFighter.myWeapon.myPower;
            defenderTotalDefence = aInactiveFighter.myDefence + aInactiveFighter.myArmour.myDefence;

            //Adjust damage rate based on damage and armour types used
            switch (aActiveFighter.myWeapon.myDamageType)
            {
                case DamageType.Slashing:
                    switch (aInactiveFighter.myArmour.myArmourType)
                    {
                        case ArmourType.Heavy:
                            damageRate *= 0.5f;
                            break;
                        case ArmourType.Medium:
                            damageRate *= 1f;
                            break;
                        case ArmourType.Light:
                            damageRate *= 2f;
                            break;
                        case ArmourType.Magical:
                            damageRate *= 1f;
                            break;
                        default:
                            break;
                    }
                    break;
                case DamageType.Blunt:
                    switch (aInactiveFighter.myArmour.myArmourType)
                    {
                        case ArmourType.Heavy:
                            damageRate *= 2f;
                            break;
                        case ArmourType.Medium:
                            damageRate *= 0.5f;
                            break;
                        case ArmourType.Light:
                            damageRate *= 1f;
                            break;
                        case ArmourType.Magical:
                            damageRate *= 1f;
                            break;
                        default:
                            break;
                    }
                    break;
                case DamageType.Piercing:
                    switch (aInactiveFighter.myArmour.myArmourType)
                    {
                        case ArmourType.Heavy:
                            damageRate *= 1f;
                            break;
                        case ArmourType.Medium:
                            damageRate *= 2f;
                            break;
                        case ArmourType.Light:
                            damageRate *= 0.5f;
                            break;
                        case ArmourType.Magical:
                            damageRate *= 1f;
                            break;
                        default:
                            break;
                    }
                    break;
                case DamageType.Magical:
                    damageRate *= 1f;
                    break;
                default:
                    break;
            }

            //Adjust damage rate based on attack vs defence
            if (attackerTotalPower >= (2 * defenderTotalDefence))
            {
                damageRate *= 2f;
            }
            else if (attackerTotalPower <= (2 / defenderTotalDefence))
            {
                damageRate *= 0.5f;
            }
            else
            {
                damageRate *= 1f;
            }

            float damageDealt = attackerTotalPower * damageRate;

            //aFigher0 deals damage to aFighter1
        }
        else    //Active fighter = 1
        {
            myActiveFighter = false;
        }

        if (myActiveFighter == false)
        {
            myActiveFighter = true;
        }
        else
        {
            myActiveFighter = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
