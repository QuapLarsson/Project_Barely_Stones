using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [Header("Setup")]
    public GameObject characterInfo;
    [HideInInspector]
    private string name;
    private int level;
    private int experience;
    private int expToLevel;
    private int currentHP;
    private int maxHP;
    private int power;
    private int defense;
    private int movement;
    private int luck;

    public void CharacterDisplay(CharacterStats.CharacterType characterType)
    {
        PlayerStats.instance.GetCharacterStats(characterType);
    }
}
