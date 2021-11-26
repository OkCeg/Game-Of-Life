using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        //defaults to time scale of 1
        slider.value = 10;
    }

    public void SetTimeScale(float sliderValue)
    {
        //update pause button
        Pause.NormalTimeScale = sliderValue / 10;

        //if game is not paused, also update the actual time scale
        if (!Pause.Paused)
        {
            Time.timeScale = Pause.NormalTimeScale;
        }
    }
}
