using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Party Info")]
    public int mainCharacterLevel = 1;
    public int secondCharacterLevel = 1;
    public int thirdCharacterLevel = 1;
    public int fourthCharacterLevel = 1;
    
    [HideInInspector]
    public static PlayerStats instance;
    private CharacterStats generatedStats;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public CharacterStats GetCharacterStats(CharacterStats.CharacterType characterType)
    {
        generatedStats = new CharacterStats();
        generatedStats.GenerateStats(characterType);
        return generatedStats;
    }
    public CharacterStats GetCharacterStats(CharacterStats.CharacterType characterType, int level)
    {
        generatedStats = new CharacterStats();
        generatedStats.GenerateStats(characterType, level);
        return generatedStats;
    }
}
