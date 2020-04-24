using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue, pitch);
    }
}
