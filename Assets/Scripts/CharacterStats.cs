using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public enum CharacterType
    {
        MainCharacter,
        MemberTwo,
        MemberThree,
        MemberFour,
        EnemyOne
    }
    public CharacterType characterType;
    public int level;
    public int movement;
    public int health;
    public int power;
    public int defense;
    public int luck;
    private float healthGrowth;
    private float powerGrowth;
    private float defenseGrowth;
    private float luckGrowth;

    internal void GenerateStats(CharacterType _characterType)
    {
        characterType = _characterType;
        NewStats();
    }

    internal void GenerateStats(CharacterType _characterType, int _level)
    {
        characterType = _characterType;
        level = _level;
        NewStats();
    }
    private void NewStats()
    {
        switch (characterType)
        {
            case CharacterType.MainCharacter:
                level = PlayerStats.instance.activePartyData.mainCharacterLevel;
                movement = 5;
                health = 10;
                power = 3;
                defense = 3;
                luck = 5;
                healthGrowth = 5f;
                powerGrowth = 2f;
                defenseGrowth = 2f;
                luckGrowth = 2f;
                CalculateNewStats();
                break;
            case CharacterType.MemberTwo:
                level = PlayerStats.instance.activePartyData.secondCharacterLevel;
                movement = 3;
                health = 20;
                power = 4;
                defense = 4;
                luck = 1;
                healthGrowth = 10f;
                powerGrowth = 3f;
                defenseGrowth = 3f;
                luckGrowth = 0.5f;
                CalculateNewStats();
                break;
            case CharacterType.MemberThree:
                level = PlayerStats.instance.activePartyData.thirdCharacterLevel;
                movement = 7;
                health = 7;
                power = 6;
                defense = 1;
                luck = 8;
                healthGrowth = 3f;
                powerGrowth = 5f;
                defenseGrowth = 1f;
                luckGrowth = 3f;
                CalculateNewStats();
                break;
            case CharacterType.MemberFour:
                level = PlayerStats.instance.activePartyData.fourthCharacterLevel;
                movement = 6;
                health = 10;
                power = 2;
                defense = 2;
                luck = 3;
                healthGrowth = 4f;
                powerGrowth = 4f;
                defenseGrowth = 2f;
                luckGrowth = 2.5f;
                CalculateNewStats();
                break;
            case CharacterType.EnemyOne:
                movement = 4;
                health = 20;
                power = 4;
                defense = 3;
                luck = 1;
                healthGrowth = 10f;
                powerGrowth = 3f;
                defenseGrowth = 2f;
                luckGrowth = 1f;
                CalculateNewStats();
                break;
        }
    }
    private void CalculateNewStats()
    {
        health += (int)(healthGrowth * level);
        power += (int)(powerGrowth * level);
        defense += (int)(defenseGrowth * level);
        luck += (int)(luckGrowth * level);
    }
}
