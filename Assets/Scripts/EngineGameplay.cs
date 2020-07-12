using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineGameplay : MonoBehaviour
{
    public FuelTank leftTank;
    public FuelTank centerTank;
    public FuelTank rightTank;

    public TurnableWheel leftWheel;
    public TurnableWheel centerWheel;
    public TurnableWheel rightWheel;

    // Start is called before the first frame update
    void Start()
    {
        leftTank.SetTankValue(Random.Range(0, 101));
        centerTank.SetTankValue(Random.Range(0, 101));
        rightTank.SetTankValue(Random.Range(0, 101));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
