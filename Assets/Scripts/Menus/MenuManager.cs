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
    public CharacterMenu charMenu;
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
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InSaveMenu) &&
            !PauseManager.IsPauseState(PauseManager.PauseState.InDialogue))
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
        else if (PauseManager.IsPauseState(PauseManager.PauseState.InSaveMenu))
        {
            SaveMenu.instance.CloseMenu();
        }
    }

    public void ActivateMenu()
    {
        if (!PauseManager.IsPauseState(PauseManager.PauseState.InDialogue)
            && !PauseManager.IsPauseState(PauseManager.PauseState.InSaveMenu))
        {
            //gamePaused = true;
            PauseManager.SetPauseState(PauseManager.PauseState.InMenu);
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
        //gamePaused = false;
        PauseManager.SetPauseState(PauseManager.PauseState.Playing);
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
        charMenu.OnClick(1);
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
