﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Fighter myActiveFighter;
    public Fighter myInactiveFighter;
    public GameObject myButton;
    bool myIsAnimating = false;
    float myAnimationTimer = 2f;

    //TODO: Abilities
    //TODO: Critical Hit
    //TODO: Deal Damage
    //TODO: Animations (Hops and Shakes)
    //TODO: UI Elements (HP, EXP, Abilities)
    //TODO: make damage rate change depending on how much larger/smaller power is than damage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Take in two fighters and start the battle. Entry point for battles.
    public void Init(Fighter aActiveFighter, Fighter aInactiveFighter)
    {
        myActiveFighter = aActiveFighter;
        myInactiveFighter = aInactiveFighter;

        //TODO: Shift camera and present battle scene ???

        DealDamage(myActiveFighter, myInactiveFighter);

        //Reset scene to strategy mode
    }

    public void OnClick()
    {
        if (myIsAnimating == false)
        {
            Init(myActiveFighter, myInactiveFighter);
        }
    }

    //Start battle sequence. Kapow.
    void DealDamage(Fighter aActiveFighter, Fighter aInactiveFighter)
    {
        float damageRate = 1f;
        int attackerTotalPower;
        int defenderTotalDefence;

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

        aActiveFighter.transform.Translate(new Vector3(-0.3f, 0, 0));
        myIsAnimating = true;
        if (aInactiveFighter.TakeDamage((int)damageDealt))
        {
            aInactiveFighter.Die();
            //Destroy(aInactiveFighter.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myIsAnimating == true)
        {
            myAnimationTimer -= Time.deltaTime;
            if (myAnimationTimer <= 0f)
            {
                myIsAnimating = false;
                myAnimationTimer = 2f;
                myActiveFighter.gameObject.transform.Translate(new Vector3(0.3f, 0, 0));
            }
        }
    }
}
