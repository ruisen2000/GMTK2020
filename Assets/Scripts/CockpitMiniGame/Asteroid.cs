using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public CircleCollider2D col;

    [SerializeReference]
    int selectedAsteroid = 0;

    [SerializeReference]
    Sprite[] sprites;

    [SerializeReference]
    float[] asteroidColliderSizes;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    bool DebugMode = false;

    [SerializeField]
    float rotationRateMin = 0;

    [SerializeField]
    float rotationRateMax = 0;


    float rotationRate = 0;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();

        if(asteroidColliderSizes.Length != sprites.Length)
        {
            //Well thats bad, throw an error I guess?
            Debug.LogError("Asteroid Sprites and sizes dont match up !");
        }

        selectedAsteroid = UnityEngine.Random.Range(0, sprites.Length - 1);

        if(selectedAsteroid > sprites.Length - 1)
        {
            selectedAsteroid = 0;
        }



        spriteRenderer.sprite = sprites[selectedAsteroid];
        col.radius = asteroidColliderSizes[selectedAsteroid];


        transform.eulerAngles = new Vector3(0,0,UnityEngine.Random.Range(0, 360));

        rotationRate = UnityEngine.Random.Range(rotationRateMin, rotationRateMax);

    }

    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationRate * Time.deltaTime);

    }
}
