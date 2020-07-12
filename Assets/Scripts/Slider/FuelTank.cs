using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTank : MonoBehaviour
{
    public Slider slider;
    public float SLIDER_MAX = 100.0f;

    public void SetTankValue(int value)
    {
        slider.value = value;
    }


    public void IncrementTank(float value)
    {
        if (slider.value < SLIDER_MAX || value < 0)
        {
            slider.value += value;
        }           
    }

    public int GetTankValue(int value)
    {
        return (int)slider.value;
    }
}
