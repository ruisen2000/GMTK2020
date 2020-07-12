using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTank : MonoBehaviour
{
    public Slider slider;

    public void SetTankValue(int value)
    {
        slider.value = value;
    }

    public int GetTankValue(int value)
    {
        return (int)slider.value;
    }
}
