using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour {
    [SerializeField] private float energyLevel = 1.0f;

    [SerializeField] private float decay = 0.05f;

    [SerializeField] private Image fillMeter;
    
    
    public delegate void OnEnergyZero();
    public static event OnEnergyZero OnEnergyZeroEvent;

    private void OnEnable() {
        FuelRod.OnFuelRodInsertEvent += IncreaseEnergy;
    }

    private void OnDisable() {
        FuelRod.OnFuelRodInsertEvent -= IncreaseEnergy;
    }

    private void IncreaseEnergy(bool success) {
        if(success)
            energyLevel += 0.15f;
    }
    
    void Update() {
        if (energyLevel <= 0) {
            energyLevel = 0;

            if (OnEnergyZeroEvent != null)
                OnEnergyZeroEvent();
        }
        energyLevel = energyLevel - (decay * Time.deltaTime);
        fillMeter.fillAmount = energyLevel;

    }
}
