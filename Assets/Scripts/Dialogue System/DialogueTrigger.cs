using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //public Dialogue dialogue;
    public DialogueTree dialogueTree;
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogueTree.dialogues/*, secondaryDialogue, hasSecondaryDialogue*/);
    }
}
