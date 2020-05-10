using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    [Header("Setup")]
    //public GameObject characterInfo;
    public TMP_Text charName;
    public TMP_Text charLevel;
    public TMP_Text charEXP;
    public TMP_Text charTNL;
    public TMP_Text charHealth;
    public TMP_Text charPower;
    public TMP_Text charDefense;
    public TMP_Text charMovement;
    public TMP_Text charLuck;
    public TMP_Text charWeapon;
    public TMP_Text charArmor;

    public void CharacterDisplay(CharacterStats.CharacterType characterType)
    {
        CharacterStats newStats = PlayerStats.instance.GetCharacterStats(characterType);
        charName.text = newStats.name;
        //charLevel.text = newStats.level.ToString();
        //charEXP.text = newStats.experience.ToString();
        charHealth.text = "XXXX" + " / " + newStats.health.ToString();
        charPower.text = newStats.power.ToString();
        charDefense.text = newStats.defense.ToString();
        charMovement.text = newStats.movement.ToString();
        charLuck.text = newStats.luck.ToString();
        charWeapon.text = newStats.weaponType.ToString();
        charArmor.text = newStats.armorType.ToString();
    }

    public void OnClick(int buttonNumber)
    {
        if (buttonNumber == 1)
        {
            CharacterDisplay(CharacterStats.CharacterType.MainCharacter);
        }
        else if (buttonNumber == 2)
        {
            CharacterDisplay(CharacterStats.CharacterType.MemberTwo);
        }
        else if (buttonNumber == 3)
        {
            CharacterDisplay(CharacterStats.CharacterType.MemberThree);
        }
        else if (buttonNumber == 4)
        {
            CharacterDisplay(CharacterStats.CharacterType.MemberFour);
        }

    }
}
