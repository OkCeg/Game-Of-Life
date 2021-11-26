using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static float NormalTimeScale = 1;

    public static bool Paused = true;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ChangeTime()
    {
        if (Paused)
        {
            //unpause
            Paused = false;
            Time.timeScale = NormalTimeScale;
        }
        else
        {
            //pause
            Paused = true;
            Time.timeScale = 0;
        }
    }
}
