using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject pauseMenu;
    public Animator menuAnimator;
    public Animator charMenuAnimator;

    [HideInInspector]
    public bool gamePaused;
    public static MenuManager instance;
    public enum MenuState
    {
        Inactive,
        Main,
        Characters,
        Inventory,
        AdventureLog,
        Settings,
        ExitGame
    }
    public MenuState currentMenuState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //MakeInactive();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButton();
        }
    }

    
    private void EscapeButton()
    {
        switch (currentMenuState)
        {
            case MenuState.Inactive:
                ActivateMenu();
                break;
            case MenuState.Main:
                DeactivateMenu();
                break;
            case MenuState.Characters:
                CloseCharacters();
                break;
            case MenuState.Inventory:
                CloseInventory();
                break;
            case MenuState.AdventureLog:
                CloseAdventureLog();
                break;
            case MenuState.Settings:
                CloseSettings();
                break;
            case MenuState.ExitGame:
                CloseExitGame();
                break;
        }
    }

    public void ActivateMenu()
    {
        if (!DialogueManager.instance.boxAnimator.GetBool("isOpen")
            && !SaveMenu.instance.animator.GetBool("isOpen"))
        {
            gamePaused = true;
            menuAnimator.SetTrigger("OpenMenu");
            currentMenuState = MenuState.Main;
        }
    }
    public void ReActivateMenu()
    {
            menuAnimator.SetTrigger("OpenMenu");
            currentMenuState = MenuState.Main;
    }

    private void DeactivateMenu()
    {
        gamePaused = false;
        menuAnimator.SetTrigger("CloseMenu");
        currentMenuState = MenuState.Inactive;
    }
    public void HideMenu()
    {
        menuAnimator.SetTrigger("CloseMenu");
    }
    public void MakeActive()
    {
        pauseMenu.SetActive(true);
    }
    public void MakeInactive()
    {
        pauseMenu.SetActive(false);
    }
    public void OpenCharacters()
    {
        charMenuAnimator.SetTrigger("OpenMenu");
        currentMenuState = MenuState.Characters;
    }

    public void OpenInventory()
    {
        throw new NotImplementedException();
    }

    public void OpenAdventureLog()
    {
        throw new NotImplementedException();
    }

    public void OpenSettings()
    {
        throw new NotImplementedException();
    }

    public void OpenExitGame()
    {
        throw new NotImplementedException();
    }

    private void CloseCharacters()
    {
        charMenuAnimator.SetTrigger("CloseMenu");
    }

    private void CloseInventory()
    {
        throw new NotImplementedException();
    }

    private void CloseAdventureLog()
    {
        throw new NotImplementedException();
    }

    private void CloseSettings()
    {
        throw new NotImplementedException();
    }

    private void CloseExitGame()
    {
        throw new NotImplementedException();
    }
}
