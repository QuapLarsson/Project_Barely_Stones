using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string leftName;
    public bool leftNameOn;
    public string rightName;
    public bool rightNameOn;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    [TextArea(5, 5)]
    public string[] textBatches;
    public DialogueManager.TextSpeeds textSpeed;
}
