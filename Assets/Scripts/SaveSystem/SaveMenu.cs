using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [Header("Setup")]
    public Animator animator;
    public Animator headerAnimator;
    public static SaveMenu instance;
    [HideInInspector]
    public SavePoint activeSavePoint;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OpenMenu(activeSavePoint);
        }
    }

    public void RequestSave(int _saveSlot)
    {
        SaveManager.SaveData(activeSavePoint, _saveSlot);
    }

    public void OpenMenu(SavePoint _savePoint)
    {
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InDialogue)
            && !PauseManager.IsPauseState(PauseManager.PauseState.InMenu))
        {

            if (!animator.GetBool("isOpen"))
            {
                PauseManager.SetPauseState(PauseManager.PauseState.InSaveMenu);
                animator.SetTrigger("EnterMenu");
                headerAnimator.SetTrigger("Open");
            }
            else
            {
                animator.SetTrigger("ExitMenu");
                headerAnimator.SetTrigger("Close");
                PauseManager.SetPauseState(PauseManager.PauseState.Playing);
            }
        }
    }
}
