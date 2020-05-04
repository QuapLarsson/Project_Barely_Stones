using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject DialogueCanvas;
    public TMP_Text nameTextLeft;
    public GameObject leftName;
    public TMP_Text nameTextRight;
    public GameObject rightName;
    public TMP_Text dialogueText;
    public GameObject yesNoBox;
    public TMP_Text optionOneText;
    public TMP_Text optionTwoText;

    public Animator boxAnimator;
    public AudioSource audioSource;
    //[Range(1, 5)]
    //public int textSpeedSetting=1;
    public TextSpeeds currentTextSpeed;
    [HideInInspector]
    private Dialogue newDialogue;
    private Queue<string> textBatches;
    private Queue<Dialogue> dialogueQueue;
    private List<char> charsToHoldAt = new List<char> { '.','!','?'};
    public static DialogueManager instance;

    private float textSpeed;
    private bool isTyping;
    private bool leftNameOn;
    private bool rightNameOn;
    private bool continueToNextDialogue;
    private string batch;
    public enum TextSpeeds
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
    
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
        //nameTextLeft.alpha = 0;
        //nameTextRight.alpha = 0;
        leftName.SetActive(false);
        rightName.SetActive(false);
        yesNoBox.SetActive(false);
        dialogueText.alpha = 0;
    }

    void Start()
    {
        textBatches = new Queue<string>();
        dialogueQueue = new Queue<Dialogue>();
    }

    public void ChangeTextSpeed(TextSpeeds speed)
    {
        currentTextSpeed = speed;
        SetTextSpeed();
    }
    internal void SetTextSpeed()
    {
        switch (currentTextSpeed)
        {
            case TextSpeeds.One:
                textSpeed = 0.5f;
                break;
            case TextSpeeds.Two:
                textSpeed = 0.1f;
                break;
            case TextSpeeds.Three:
                textSpeed = 0.05f;
                break;
            case TextSpeeds.Four:
                textSpeed = 0.02f;
                break;
            case TextSpeeds.Five:
                textSpeed = 0.01f;
                break;
        }
    }
    /// <summary>
    /// Called on from DialogueTrigger to begin.
    /// Only begins if a dialogue isn't already active.
    /// Begins open animation for boxAnimator, which once finished changes bool isOpen to true.
    /// Adds all text batches from dialogue to queue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue[] dialogueTree/*Dialogue dialogue, Dialogue _secondary, bool _secondaryTrue*/)
    {
        
        if (!boxAnimator.GetBool("isOpen"))
        {
            foreach (Dialogue dialogue in dialogueTree)
            {
                boxAnimator.SetTrigger("OpenStart");
                dialogueQueue.Enqueue(dialogue);


            }

            newDialogue = dialogueQueue.Dequeue();

            audioSource.pitch = newDialogue.pitch;
            //boxAnimator.SetTrigger("OpenStart");
            nameTextLeft.text = newDialogue.leftName;
            nameTextRight.text = newDialogue.rightName;
            leftNameOn = newDialogue.leftNameOn;
            rightNameOn = newDialogue.rightNameOn;
            textBatches.Clear();
            ChangeTextSpeed(newDialogue.textSpeed);

            foreach (string textBatch in newDialogue.textBatches)
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
            CheckNames();
            
            dialogueText.alpha = 255;
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = batch;
                isTyping = false;
            }
            else
            {
                if (textBatches.Count == 0)
                {
                    NextDialogue();
                    return;
                }

                batch = textBatches.Dequeue();
                StartCoroutine(TypingEffect(batch));
            }
        }
    }
    /// <summary>
    /// Handles whether or not the left, right, both, or neither name boxes should be used.
    /// Based on info from Dialogue.
    /// </summary>
    private void CheckNames()
    {
        if (rightNameOn)
        {
            rightName.SetActive(true);
        }
        else
        {
            rightName.SetActive(false);
        }

        if (leftNameOn)
        {
            leftName.SetActive(true);
        }
        else
        {
            leftName.SetActive(false);
        }
    }

    public void NextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        newDialogue = dialogueQueue.Dequeue();

        audioSource.pitch = newDialogue.pitch;
        nameTextLeft.text = newDialogue.leftName;
        nameTextRight.text = newDialogue.rightName;
        leftNameOn = newDialogue.leftNameOn;
        rightNameOn = newDialogue.rightNameOn;
        textBatches.Clear();
        ChangeTextSpeed(newDialogue.textSpeed);

        foreach (string textBatch in newDialogue.textBatches)
        {
            textBatches.Enqueue(textBatch);
        }
        DisplayNextTextBatch();
    }
    /// <summary>
    /// Goes through each char in text batch and types them out with a delay.
    /// Delay can be changed with currentTextSpeed.
    /// Plays SFX when typing out a letter, except for spaces.
    /// SFX pitch can be changed in DialogueTrigger.
    /// Pauses longer if current char is in list: charsToHoldAt.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator TypingEffect (string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            if (letter != ' ')
            {
                audioSource.Play();
            }
            float wait;
            if (charsToHoldAt.Contains(letter))
            {
                wait = 1f;
            }
            else
            {
                wait = textSpeed;
            }
                yield return new WaitForSecondsRealtime(wait);
        }
        isTyping = false;
    }
    /// <summary>
    /// Changes text alphas to 0 to hide before closing animation.
    /// Triggers closing animation, which changes animator's bool isOpen to false once completed.
    /// </summary>
    public void EndDialogue()
    {
        leftName.SetActive(false);
        rightName.SetActive(false);
        dialogueText.alpha = 0;
        boxAnimator.SetTrigger("CloseStart");
    }

    
    //public void NewDialogue()
    //{
    //    audioSource.pitch = secondaryDialogue.pitch;
    //    nameTextLeft.text = secondaryDialogue.leftName;
    //    nameTextRight.text = secondaryDialogue.rightName;
    //    leftNameOn = secondaryDialogue.leftNameOn;
    //    rightNameOn = secondaryDialogue.rightNameOn;
    //    textBatches.Clear();
    //    ChangeTextSpeed(secondaryDialogue.textSpeed);
    //    foreach (string textBatch in secondaryDialogue.textBatches)
    //    {
    //        textBatches.Enqueue(textBatch);
    //    }
    //    DisplayNextTextBatch();
    //}
}
