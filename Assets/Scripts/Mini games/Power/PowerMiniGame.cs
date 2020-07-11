using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMiniGame : Minigame {

    [SerializeField] 
    private GameObject m_FuelRod;
    
    [SerializeField] 
    private GameObject m_ReactorCore;

    private void InitPowerMiniGame() {
        m_Name = "Power";
    }

    private void Awake() {
        InitPowerMiniGame();
    }
 
}
