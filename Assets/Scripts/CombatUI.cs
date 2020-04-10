using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatUI : MonoBehaviour
{
    public TextMeshProUGUI currentTurnText;

    public void UpdateText(int turnCount)
    {
        currentTurnText.text = string.Format("Turn: {0}", turnCount);
    }
}
