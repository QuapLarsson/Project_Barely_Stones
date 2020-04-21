using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject DialogueCanvas;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator boxAnimator;
    [HideInInspector]
    private Queue<string> textBatches;
    public static DialogueManager instance;
    /// <summary>
    /// Creates a static instance of DialogueManager to use when calling on methods from outside.
    /// Changes text alphas to 0 as default so text is not visible during dialogue open animation.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            
            instance = this;
        }
        else
        {
            Debug.LogWarning("DialogueManager instance already exists.");
        }
        nameText.alpha = 0;
        dialogueText.alpha = 0;
    }

    void Start()
    {
        textBatches = new Queue<string>();
    }
    /// <summary>
    /// Called on from DialogueTrigger to begin.
    /// Only begins if a dialogue isn't already active.
    /// Begins open animation for boxAnimator, which once finished changes bool isOpen to true.
    /// Adds all text batches from dialogue to queue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        if (!boxAnimator.GetBool("isOpen"))
        {
            boxAnimator.SetTrigger("OpenStart");
            nameText.text = dialogue.name;
            textBatches.Clear();

            foreach (string textBatch in dialogue.textBatches)
            {
                textBatches.Enqueue(textBatch);
            }
        }
    }
    /// <summary>
    /// Only reacts when animator's bool isOpen is true.
    /// Changes text alphas to full as animation has finished.
    /// Called first by animator when entering DialogueBoxOpened state.
    /// Called again from outside to proceed to next text batch in queue. Use button on click event or similar.
    /// If queue is empty calls EndDialogue. 
    /// </summary>
    public void DisplayNextTextBatch()
    {
        if (boxAnimator.GetBool("isOpen"))
        {
            nameText.alpha = 255;
            dialogueText.alpha = 255;
            if (textBatches.Count == 0)
            {
                EndDialogue();
                return;
            }

            string batch = textBatches.Dequeue();
            dialogueText.text = batch;
        }
    }
    /// <summary>
    /// Changes text alphas to 0 to hide before closing animation.
    /// Triggers closing animation, which changes animator's bool isOpen to false once completed.
    /// </summary>
    public void EndDialogue()
    {
        nameText.alpha = 0;
        dialogueText.alpha = 0;
        boxAnimator.SetTrigger("CloseStart");
    }
}
