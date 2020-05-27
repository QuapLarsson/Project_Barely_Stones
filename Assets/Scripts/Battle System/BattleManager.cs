using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Fighter m_ActiveFighter;
    public Fighter m_InactiveFighter;
    GameObject m_EnemyStandin;
    public bool m_IsAnimating = false;
    public CameraControllerScript m_MainCamera;

    //UI Start
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
    private void Awake()
    {
        m_InactiveFighterHPBar.gameObject.SetActive(false);
        m_ActiveFighterHPBar.gameObject.SetActive(false);
    }

    //Take in two fighters and start the battle. Entry point for battles.
    public IEnumerator Init(Fighter a_ActiveFighter, GameObject a_Enemy)
    {
        //Assign fighters
        m_ActiveFighter = a_ActiveFighter;
        m_InactiveFighter = a_Enemy.GetComponent<Fighter>();
        m_EnemyStandin = a_Enemy;

        //Initialize HP bars
        m_InactiveFighterHPBar.maxValue = m_InactiveFighter.myMaxHP;
        m_InactiveFighterHPBar.value = m_InactiveFighter.myCurrentHP;
        m_InactiveFighterHPBar.minValue = 0;
        m_ActiveFighterHPBar.maxValue = m_ActiveFighter.myMaxHP;
        m_ActiveFighterHPBar.value = m_ActiveFighter.myCurrentHP;
        m_ActiveFighterHPBar.minValue = 0;

        //Makes combatants face each other and moves the camera
        m_ActiveFighter.gameObject.transform.LookAt(m_InactiveFighter.gameObject.transform, Vector3.up);
        m_InactiveFighter.gameObject.transform.LookAt(m_ActiveFighter.gameObject.transform, Vector3.up);
        m_MainCamera.ShiftCameraToBattle(m_ActiveFighter.transform, m_InactiveFighter.transform);
        yield return new WaitForSeconds(1); //Wait for camera to move

        //Show HP bars and set animating flag
        m_InactiveFighterHPBar.gameObject.SetActive(true);
        m_ActiveFighterHPBar.gameObject.SetActive(true);
        m_IsAnimating = true;

        //Start damage calculation
        DealDamage(m_ActiveFighter, m_InactiveFighter);
        yield return new WaitForSeconds(3); //Wait for attack animations (To be implemented)

        //Restore camera and hide HP bars
        m_MainCamera.RestoreCamera();
        m_ActiveFighterHPBar.gameObject.SetActive(false);
        m_InactiveFighterHPBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1); //Wait for camera to move

        //Clear animation flag and exit method
        m_IsAnimating = false;
        yield return 0;
    }

    //Initialize battle from the enemy attack queue
    public IEnumerator Init(ActionQueueEntry a_Entry)
    {
        //Assign fighters
        m_ActiveFighter = a_Entry.GetEnemyFighter();
        m_InactiveFighter = a_Entry.GetPCFighter();
        m_EnemyStandin = a_Entry.GetPCFighter().gameObject;

        //Initialize HP bars
        m_InactiveFighterHPBar.maxValue = m_InactiveFighter.myMaxHP;
        m_InactiveFighterHPBar.value = m_InactiveFighter.myCurrentHP;
        m_InactiveFighterHPBar.minValue = 0;
        m_ActiveFighterHPBar.maxValue = m_ActiveFighter.myMaxHP;
        m_ActiveFighterHPBar.value = m_ActiveFighter.myCurrentHP;
        m_ActiveFighterHPBar.minValue = 0;

        //Makes combatants face each other and moves the camera
        m_ActiveFighter.gameObject.transform.LookAt(m_InactiveFighter.gameObject.transform, Vector3.up);
        m_InactiveFighter.gameObject.transform.LookAt(m_ActiveFighter.gameObject.transform, Vector3.up);
        m_MainCamera.ShiftCameraToBattle(m_ActiveFighter.transform, m_InactiveFighter.transform);
        yield return new WaitForSeconds(1); //Wait for camera to move

        //Show HP bars and set animating flag
        m_InactiveFighterHPBar.gameObject.SetActive(true);
        m_ActiveFighterHPBar.gameObject.SetActive(true);
        m_IsAnimating = true;

        //Start damage calculation
        DealDamage(m_ActiveFighter, m_InactiveFighter);
        yield return new WaitForSeconds(3); //Wait for attack animations (To be implemented)
        
        //Restore camera and hide HP bars
        m_MainCamera.RestoreCamera();
        m_ActiveFighterHPBar.gameObject.SetActive(false);
        m_InactiveFighterHPBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1); //Wait for camera to move

        //Clear animation flag and exit method
        m_IsAnimating = false;
        yield return 0;
    }

    public void OnClick()
    {
        DealDamage(m_ActiveFighter, m_InactiveFighter);
    }

    //Start battle sequence. Kapow.
    void DealDamage(Fighter a_ActiveFighter, Fighter a_InactiveFighter)
    {
        float damageRate = 1f;
        int attackerTotalPower;
        int defenderTotalDefence;
        a_ActiveFighter.AttackEnemy(true);
        attackerTotalPower = a_ActiveFighter.myPower;
        defenderTotalDefence = a_InactiveFighter.myDefence;
        
        {
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
        
        StartCoroutine(InactiveFighterHPBarDepletion(a_InactiveFighter.myCurrentHP - damageDealt));
        if (a_InactiveFighter.TakeDamage((int)damageDealt))
        {
            Destroy(m_EnemyStandin);
            a_InactiveFighter.Die();
        }
        a_InactiveFighter.animateDamage = false;
        a_ActiveFighter.AttackEnemy(false);
        return;
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
        }

        if (m_InactiveFighterHPBar.value <= 0)
        {
            Destroy(m_InactiveFighterHPBar.transform.GetChild(1).GetChild(0).gameObject);
        }
        yield return 0;
    }
}
