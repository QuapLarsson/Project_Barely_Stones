using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [Header("Setup")]
    public Animator animator;
    public Animator headerAnimator;
    public Animator confirmAnimator;
    public static SaveMenu instance;
    [HideInInspector]
    public SavePoint activeSavePoint;
    private int saveSlot;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RequestSave(int _saveSlot)
    {
        SaveManager.SavePlayerData(activeSavePoint, /*PlayerStats.instance.activePartyData, PlayerStats.instance.activeInventoryData,*/ _saveSlot);
        CloseMenu();
    }

    public void PickSlot(int _saveSlot)
    {
        confirmAnimator.SetBool("isOpen", true);
        saveSlot = _saveSlot;
    }

    public void Confirm()
    {
        //Debug.Log("Requesting save for slot: " + saveSlot);
        RequestSave(saveSlot);
    }

    public void Cancel()
    {
        confirmAnimator.SetBool("isOpen", false);
    }

    public void OpenMenu(SavePoint _savePoint)
    {
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InDialogue)
            && !PauseManager.IsPauseState(PauseManager.PauseState.InMenu))
        {
            activeSavePoint = _savePoint;
            if (!animator.GetBool("isOpen"))
            {
                PauseManager.SetPauseState(PauseManager.PauseState.InSaveMenu);
                animator.SetTrigger("EnterMenu");
                headerAnimator.SetTrigger("Open");
            }
        }
    }

    public void CloseMenu()
    {
        if (PauseManager.IsPauseState(PauseManager.PauseState.InSaveMenu))
        {
            animator.SetTrigger("ExitMenu");
            headerAnimator.SetTrigger("Close");
            confirmAnimator.SetBool("isOpen", false);
            PauseManager.SetPauseState(PauseManager.PauseState.Playing);
        }
    }
}
