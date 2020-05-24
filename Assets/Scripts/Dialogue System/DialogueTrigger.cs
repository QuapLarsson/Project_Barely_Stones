using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Barely.AI.Movement;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogueTree;
    public void TriggerDialogue()
    {
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InDialogue)
            && !DialogueManager.instance.boxAnimator.GetBool("isOpen"))
        {
            NavMovement[] movement = (NavMovement[])FindObjectsOfType(typeof(NavMovement));
            for (int i = 0; i < movement.Length; i++)
            {
                movement[i].ResetPath();
            }
                DialogueManager.instance.StartDialogue(dialogueTree.dialogues);
        }
    }
}
