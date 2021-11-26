using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        //doesn't directly take from time scale because it shouldn't show x0 when paused
        text.text = "Time Scale: x" + Pause.NormalTimeScale;
    }
}