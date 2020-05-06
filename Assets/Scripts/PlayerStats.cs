using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Info")]
    public PartyData activePartyData;
    public InventoryData activeInventoryData;
    
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

    private void Start()
    {
        activePartyData = new PartyData();
        activeInventoryData = new InventoryData();
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
