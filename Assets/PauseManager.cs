using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    public enum PauseState
    {
        Playing,
        Paused,
        InMenu,
        InSaveMenu,
        InDialogue
    }
    public static PauseState pauseState;

    public static void SetPauseState(PauseState state)
    {
        pauseState = state;
    }

    public static bool IsPauseState(PauseState state)
    {
        if (state == pauseState)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
