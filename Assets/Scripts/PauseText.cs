using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (Pause.Paused)
        {
            text.text = "Unpause";
        }
        else
        {
            text.text = "Pause";
        }
    }
}
