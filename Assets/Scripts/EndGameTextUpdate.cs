using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTextUpdate : MonoBehaviour
{
    [SerializeReference]
    Text distanceScore;

    // Start is called before the first frame update
    void Start()
    {
        distanceScore.text = DataHolder.allData.distanceTravelled.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
