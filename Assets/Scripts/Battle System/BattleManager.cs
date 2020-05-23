using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Fighter m_ActiveFighter;
    public Fighter m_InactiveFighter;
    GameObject m_EnemyStandin;
    bool m_IsAnimating = false;
    float m_AnimationTimer = 2f;
    public Camera m_MainCamera;
    public Camera m_CombatCamera;

    //UI Start
    public GameObject m_StartFightButton;
    public Slider m_ActiveFighterHPBar;
    public Slider m_InactiveFighterHPBar;
    //UI End

    //TODO: Abilities
    //TODO: Critical Hit
    //TODO: Deal Damage
    //TODO: Animations (Hops and Shakes)
    //TODO: UI Elements (HP, EXP, Abilities)
    //TODO: make damage rate change depending on how much larger/smaller power is than damage

    // Start is called before the first frame update
    void Start()
    {
        if (m_InactiveFighter != null)
        {
            m_InactiveFighterHPBar.maxValue = m_InactiveFighter.myMaxHP;
            m_InactiveFighterHPBar.value = m_InactiveFighter.myCurrentHP;
            m_InactiveFighterHPBar.minValue = 0;
        }
        if (m_ActiveFighter != null)
        {
            m_ActiveFighterHPBar.maxValue = m_ActiveFighter.myMaxHP;
            m_ActiveFighterHPBar.value = m_ActiveFighter.myCurrentHP;
            m_ActiveFighterHPBar.minValue = 0;
        }
    }

    //Take in two fighters and start the battle. Entry point for battles.
    public void Init(/*Fighter a_ActiveFighter, Fighter a_InactiveFighter*/ ref GameObject a_Enemy)
    {
        //m_ActiveFighter = a_ActiveFighter;
        //m_InactiveFighter = a_InactiveFighter;
        m_EnemyStandin = a_Enemy;

        //TODO: Shift camera and present battle scene ???

        //DealDamage(m_ActiveFighter, m_InactiveFighter);

        //Reset scene to strategy mode
        return;
    }

    public void OnClick()
    {
        if (m_InactiveFighter == null)
        {
            m_CombatCamera.enabled = false;
            m_MainCamera.enabled = true;
        }
        if (m_IsAnimating == false)
        {
            DealDamage(m_ActiveFighter, m_InactiveFighter);
        }
    }

    //Start battle sequence. Kapow.
    void DealDamage(Fighter a_ActiveFighter, Fighter a_InactiveFighter)
    {
        float damageRate = 1f;
        int attackerTotalPower;
        int defenderTotalDefence;

        attackerTotalPower = a_ActiveFighter.myPower;
        defenderTotalDefence = a_InactiveFighter.myDefence;

        //Adjust damage rate based on damage and armour types used
        /*switch (a_ActiveFighter.myWeapon.myDamageType)
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
        }*/

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
        StartCoroutine(InactiveFighterHPBarDepletion(a_InactiveFighter.myCurrentHP - damageDealt));
        if (a_InactiveFighter.TakeDamage((int)damageDealt))
        {
            Destroy(m_EnemyStandin);
            a_InactiveFighter.Die();
        }
        a_ActiveFighter.transform.Translate(new Vector3(-0.3f, 0, 0));
        m_IsAnimating = true;
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
        while (m_InactiveFighterHPBar.value > aTargetVal)
        {
            if (m_InactiveFighterHPBar.value - aTargetVal < 0.1f)
            {
                m_InactiveFighterHPBar.value = aTargetVal;
            }
            else
            {
                m_InactiveFighterHPBar.value -= 0.1f;
            }
            Debug.Log(m_InactiveFighterHPBar.value + ", " + aTargetVal);
            yield return null;
        }

        if (m_InactiveFighterHPBar.value <= 0)
        {
            Destroy(m_InactiveFighterHPBar.transform.GetChild(1).GetChild(0).gameObject);
        }
    }
}
