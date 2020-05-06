using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
<<<<<<< HEAD
    public Fighter m_ActiveFighter;
    public Fighter m_InactiveFighter;
    public GameObject m_EnemyStandin;
    public GameObject m_Button;
    bool m_IsAnimating = false;
    float m_AnimationTimer = 2f;
=======
    public Fighter myActiveFighter;
    public Fighter myInactiveFighter;
    bool myIsAnimating = false;
    float myAnimationTimer = 2f;
    public Camera myMainCamera;
    public Camera myCombatCamera;

    //UI Start
    public GameObject myStartFightButton;
    public Slider myActiveFighterHPBar;
    public Slider myInactiveFighterHPBar;
    //UI End
>>>>>>> parent of e0c8389d... Update BattleManager.cs

    //TODO: Abilities
    //TODO: Critical Hit
    //TODO: Deal Damage
    //TODO: Animations (Hops and Shakes)
    //TODO: UI Elements (HP, EXP, Abilities)
    //TODO: make damage rate change depending on how much larger/smaller power is than damage

    // Start is called before the first frame update
    void Start()
    {
        if (myInactiveFighter != null)
        {
            myInactiveFighterHPBar.maxValue = myInactiveFighter.myMaxHP;
            myInactiveFighterHPBar.value = myInactiveFighter.myCurrentHP;
            myInactiveFighterHPBar.minValue = 0;
        }
        if (myActiveFighter != null)
        {
            myActiveFighterHPBar.maxValue = myActiveFighter.myMaxHP;
            myActiveFighterHPBar.value = myActiveFighter.myCurrentHP;
            myActiveFighterHPBar.minValue = 0;
        }
    }

    //Take in two fighters and start the battle. Entry point for battles.
    public void Init(Fighter a_ActiveFighter, Fighter a_InactiveFighter)
    {
        m_ActiveFighter = a_ActiveFighter;
        m_InactiveFighter = a_InactiveFighter;

        //TODO: Shift camera and present battle scene ???

        DealDamage(m_ActiveFighter, m_InactiveFighter);

        //Reset scene to strategy mode
        return;
    }

    public void OnClick()
    {
<<<<<<< HEAD
        if (m_IsAnimating == false)
=======
        if (myInactiveFighter == null)
        {
            myCombatCamera.enabled = false;
            myMainCamera.enabled = true;
        }
        if (myIsAnimating == false)
>>>>>>> parent of e0c8389d... Update BattleManager.cs
        {
            Init(m_ActiveFighter, m_InactiveFighter);
        }
    }

    //Start battle sequence. Kapow.
    void DealDamage(Fighter a_ActiveFighter, Fighter a_InactiveFighter)
    {
        float damageRate = 1f;
        int attackerTotalPower;
        int defenderTotalDefence;

        attackerTotalPower = a_ActiveFighter.myPower + a_ActiveFighter.myWeapon.myPower;
        defenderTotalDefence = a_InactiveFighter.myDefence + a_InactiveFighter.myArmour.myDefence;

        //Adjust damage rate based on damage and armour types used
        switch (a_ActiveFighter.myWeapon.myDamageType)
        {
            case DamageType.Slashing:
                switch (a_InactiveFighter.myArmour.myArmourType)
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
                switch (a_InactiveFighter.myArmour.myArmourType)
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
                switch (a_InactiveFighter.myArmour.myArmourType)
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

<<<<<<< HEAD
        a_ActiveFighter.transform.Translate(new Vector3(-0.3f, 0, 0));
        m_IsAnimating = true;
        if (a_InactiveFighter.TakeDamage((int)damageDealt))
        {
            a_InactiveFighter.Die();
            Destroy(m_EnemyStandin);
            //Destroy(a_InactiveFighter.gameObject);
=======
        aActiveFighter.transform.Translate(new Vector3(-0.3f, 0, 0));
        myIsAnimating = true;
        StartCoroutine(InactiveFighterHPBarDepletion(aInactiveFighter.myCurrentHP - damageDealt));
        if (aInactiveFighter.TakeDamage((int)damageDealt))
        {
            aInactiveFighter.Die();
>>>>>>> parent of e0c8389d... Update BattleManager.cs
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsAnimating == true)
        {
            m_AnimationTimer -= Time.deltaTime;
            if (m_AnimationTimer <= 0f)
            {
                m_IsAnimating = false;
                m_AnimationTimer = 2f;
                m_ActiveFighter.gameObject.transform.Translate(new Vector3(0.3f, 0, 0));
            }
        }
    }

    IEnumerator InactiveFighterHPBarDepletion(float aTargetVal)
    {
        while (myInactiveFighterHPBar.value > aTargetVal)
        {
            if (myInactiveFighterHPBar.value - aTargetVal < 0.1f)
            {
                myInactiveFighterHPBar.value = aTargetVal;
            }
            else
            {
                myInactiveFighterHPBar.value -= 0.1f;
            }
            Debug.Log(myInactiveFighterHPBar.value + ", " + aTargetVal);
            yield return null;
        }

        if (myInactiveFighterHPBar.value <= 0)
        {
            Destroy(myInactiveFighterHPBar.transform.GetChild(1).GetChild(0).gameObject);
        }
    }
}
