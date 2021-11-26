using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRandom : MonoBehaviour
{
    //default to 25
    public static float PercentageChance = 25;
    private Text inputFieldText;

    private void Start()
    {
        inputFieldText = GetComponentInChildren<Text>();
    }

    public void ValueChanged()
    {
        if (int.TryParse(inputFieldText.text, out int value))
        {
            PercentageChance = value;
        }
        else
        {
            PercentageChance = 25;
        }
    }
}
