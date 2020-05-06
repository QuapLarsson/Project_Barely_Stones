using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Fighter m_ActiveFighter;
    public Fighter m_InactiveFighter;
    public GameObject m_EnemyStandin;
    public GameObject m_Button;
    bool m_IsAnimating = false;
    float m_AnimationTimer = 2f;

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
    public void Init(Fighter a_ActiveFighter, Fighter a_InactiveFighter)
    {
        m_ActiveFighter = a_ActiveFighter;
        m_InactiveFighter = a_InactiveFighter;

        //TODO: Shift camera and present battle scene ???

        DealDamage(m_ActiveFighter, m_InactiveFighter);

        //Reset scene to strategy mode
    }

    public void OnClick()
    {
        if (m_IsAnimating == false)
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

        a_ActiveFighter.transform.Translate(new Vector3(-0.3f, 0, 0));
        m_IsAnimating = true;
        if (a_InactiveFighter.TakeDamage((int)damageDealt))
        {
            a_InactiveFighter.Die();
            Destroy(m_EnemyStandin);
            //Destroy(a_InactiveFighter.gameObject);
        }
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
}
