using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int savePointID;
    public string savePointDescriptor;
    public int[] inventoryData;
    public int[] partyData;
    public SaveData(SavePoint _savePoint)
    {
        savePointID = _savePoint.id;
        savePointDescriptor = _savePoint.description;
        inventoryData = PlayerStats.instance.activeInventoryData.inventoryContents;
        partyData = new int[4];
        partyData[0] = PlayerStats.instance.activePartyData.mainCharacterLevel;
        partyData[1] = PlayerStats.instance.activePartyData.secondCharacterLevel;
        partyData[2] = PlayerStats.instance.activePartyData.thirdCharacterLevel;
        partyData[3] = PlayerStats.instance.activePartyData.fourthCharacterLevel;
    }
}
