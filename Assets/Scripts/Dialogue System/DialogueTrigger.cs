using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogueTree;
    public void TriggerDialogue()
    {
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InDialogue)
            && !DialogueManager.instance.boxAnimator.GetBool("isOpen"))
        {
            DialogueManager.instance.StartDialogue(dialogueTree.dialogues);
        }
    }
}
