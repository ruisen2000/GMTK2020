using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTextUpdate : MonoBehaviour
{
    [SerializeReference]
    Text distanceScore;

    [SerializeReference]
    Text hpLeft;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceScore.text = DataHolder.allData.distanceTravelled.ToString("F2") + "km";

        if(hpLeft != null)
        {
            hpLeft.text = (DataHolder.allData.shipHealth * 100.0f).ToString("F2") + "% Hull";
        }

    }
}
