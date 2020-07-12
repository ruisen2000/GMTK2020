using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRodLaunchSystem : MonoBehaviour {

    [SerializeField] private GameObject fuelRodPrefab;
    [SerializeField] private Transform fuelRodHolder;
    private void OnEnable() {
        FuelRod.OnFuelRodInsertEvent += OnFuelRodInsertSuccess;
    }

    private void OnDisable() {
        FuelRod.OnFuelRodInsertEvent -= OnFuelRodInsertSuccess;
    }
    
    void OnFuelRodInsertSuccess() {
        var position = fuelRodHolder.position;
        var newSpawnPos = new Vector3(position.x,position.y + 0.73f, position.z);
        Instantiate(fuelRodPrefab, newSpawnPos, Quaternion.identity, fuelRodHolder);
    }

}
