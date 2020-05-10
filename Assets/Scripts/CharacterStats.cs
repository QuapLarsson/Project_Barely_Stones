using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats 
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
    public string name;
    public int level;
    public int experience;
    public int movement;
    public int health;
    public int power;
    public int defense;
    public int luck;
    private float healthGrowth;
    private float powerGrowth;
    private float defenseGrowth;
    private float luckGrowth;

    public enum ArmorType
    {
        Magical,
        Heavy,
        Medium,
        Light
    }
    public ArmorType armorType;
    public enum WeaponType
    {
        Magical,
        Slashing,
        Blunt,
        Piercing
    }
    public WeaponType weaponType;

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
                name = "Main";
                level = PlayerStats.instance.activePartyData.mainCharacterLevel;
                movement = 5;
                health = 100;
                power = 3;
                defense = 3;
                luck = 5;
                healthGrowth = 80f;
                powerGrowth = 0.75f;
                defenseGrowth = 0.75f;
                luckGrowth = 0.6f;
                weaponType = WeaponType.Slashing;
                armorType = ArmorType.Medium;
                CalculateNewStats();
                break;
            case CharacterType.MemberTwo:
                name = "Tank";
                level = PlayerStats.instance.activePartyData.secondCharacterLevel;
                movement = 3;
                health = 200;
                power = 4;
                defense = 4;
                luck = 1;
                healthGrowth = 111f;
                powerGrowth = 0.75f;
                defenseGrowth = 1f;
                luckGrowth = 0.25f;
                weaponType = WeaponType.Blunt;
                armorType = ArmorType.Heavy;
                CalculateNewStats();
                break;
            case CharacterType.MemberThree:
                name = "Ranged";
                level = PlayerStats.instance.activePartyData.thirdCharacterLevel;
                movement = 7;
                health = 70;
                power = 6;
                defense = 1;
                luck = 8;
                healthGrowth = 45f;
                powerGrowth = 1f;
                defenseGrowth = 0.34f;
                luckGrowth = 0.75f;
                weaponType = WeaponType.Piercing;
                armorType = ArmorType.Light;
                CalculateNewStats();
                break;
            case CharacterType.MemberFour:
                name = "Morrigan";
                level = PlayerStats.instance.activePartyData.fourthCharacterLevel;
                movement = 6;
                health = 100;
                power = 2;
                defense = 2;
                luck = 3;
                healthGrowth = 65f;
                powerGrowth = 0.9f;
                defenseGrowth = 0.7f;
                luckGrowth = 1f;
                weaponType = WeaponType.Magical;
                armorType = ArmorType.Magical;
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
        int newHealth = health + (int)(healthGrowth * level);
        health = Mathf.Clamp(newHealth, 1, 9999);
        int newPower = power + (int)(powerGrowth * level);
        power = Mathf.Clamp(newPower, 1, 99);
        int newDefense = defense + (int)(defenseGrowth * level);
        defense = Mathf.Clamp(newDefense, 1, 99);
        int newLuck = luck + (int)(luckGrowth * level);
        luck = Mathf.Clamp(newLuck, 1 , 99);
    }
}
