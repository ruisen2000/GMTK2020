using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{

    public float distanceTravelled;
    public float shipHealth;
    public float engineFuel;
    public float reactorPower;

    public static DataHolder allData;
    // Start is called before the first frame update
    void Start()
    {
        if (allData == null)
        {
            allData = this;
        }

        distanceTravelled = 0;
        shipHealth = 0;
        engineFuel = 0;
        reactorPower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
