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
        if (Input.GetKeyDown(KeyCode.Space))
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
        if (!DialogueManager.instance.boxAnimator.GetBool("isOpen"))
        {
            if (!animator.GetBool("isOpen"))
            {
                if (!MenuManager.instance.gamePaused)
                {
                    animator.SetTrigger("EnterMenu");
                    headerAnimator.SetTrigger("Open");
                    MenuManager.instance.gamePaused = true;
                }
            }
            else
            {
                animator.SetTrigger("ExitMenu");
                headerAnimator.SetTrigger("Close");
                MenuManager.instance.gamePaused = false;
            }
        }
    }
}
