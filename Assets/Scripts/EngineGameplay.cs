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

    public float fuelGuage = 0;

    private const float TANK_DRAIN_RATE = 0.03f;


    bool tankInRange(float tankValue)
    {
        if (tankValue < 70 && tankValue > 45)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

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
        // tank delta = random flow in rate - flow out rate
        float tank1Delta;
        float tank2Delta;
        float tank3Delta;

        float wheel1 = leftWheel.getTurn();
        if (wheel1 < 0)
        { // reduce flow out
            float turnPercentage = wheel1 / 360 < -1 ? -1 : wheel1 / 360;
            tank1Delta = Random.Range(0f, 0.023f) - (TANK_DRAIN_RATE + TANK_DRAIN_RATE * turnPercentage);
        }
        else
        { // increase flow out
            float turnPercentage = wheel1 / 360 > 1 ? 10 : 10 * wheel1 / 360;
            tank1Delta = Random.Range(0f, 0.023f) - (TANK_DRAIN_RATE * turnPercentage);
            //Debug.Log("DRAIN RATE : " + TANK_DRAIN_RATE * turnPercentage);
        }

        float wheel2 = centerWheel.getTurn();
        if (wheel2 < 0)
        {
            float turnPercentage = wheel2 / 360 < -1 ? -1 : wheel2 / 360;
            tank2Delta = Random.Range(0f, 0.025f) - (TANK_DRAIN_RATE + TANK_DRAIN_RATE * turnPercentage);
        }
        else
        {
            float turnPercentage = wheel2 / 360 > 1 ? 10 : 10 * wheel2 / 360;
            tank2Delta = Random.Range(0f, 0.025f) - (TANK_DRAIN_RATE * turnPercentage);
            //Debug.Log("DRAIN RATE : " + TANK_DRAIN_RATE * turnPercentage);
        }

        float wheel3 = rightWheel.getTurn();
        if (wheel3 < 0)
        {
            float turnPercentage = wheel3 / 360 < -1 ? -1 : wheel3 / 360;
            tank3Delta = Random.Range(0f, 0.02f) - (TANK_DRAIN_RATE + TANK_DRAIN_RATE * turnPercentage);
        }
        else
        {
            float turnPercentage = wheel3 / 360 > 1 ? 10 : 10 * wheel3 / 360;
            //Debug.Log("DRAIN RATE : " + TANK_DRAIN_RATE * turnPercentage);
            tank3Delta = Random.Range(0f, 0.02f) - (TANK_DRAIN_RATE * turnPercentage);
            
        }
        
        leftTank.IncrementTank(tank1Delta);
        centerTank.IncrementTank(tank2Delta);
        rightTank.IncrementTank(tank3Delta);

        if(tankInRange(leftTank.GetTankValue()) && tankInRange(centerTank.GetTankValue()) && tankInRange(rightTank.GetTankValue()))
        {
            fuelGuage += 0.01f;
        }


    }
}
