using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CockpitMiniGame : Minigame
{
    LineRenderer path;
    List<Asteroid> asteroids;

    UnityEngine.Vector3 nextPoint;

    [SerializeReference]
    BoxCollider2D playArea;

    [SerializeReference]
    BoxCollider2D asteroidSpawnArea;

    [SerializeReference]
    CircleCollider2D playerCollider;

    [SerializeReference]
    GameObject asteroidPrefab;

    [SerializeReference]
    float asteroidSpeed = 1.0f;


    [SerializeReference]
    int maxNumberOfAsteroids = 10;

    int currentNumberOfAsteroids = 0;

    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<LineRenderer>();
        asteroids = new List<Asteroid>();
    }


    // Update is called once per frame
    void Update()
    {
        updateAsteroids();
    }

    private void updateAsteroids()
    {

        UnityEngine.Vector3 direction = nextPoint - transform.position;
        direction.x = 0;
        direction.y = 1;
        direction.Normalize();

        List<Asteroid> indexToRemove = new List<Asteroid>();

        int numberInSpawnArea = 0;
        int index = 0;
        //Find the current path point
        //Move all asteroids opposide to that direction (ie towards
        foreach(Asteroid a in asteroids)
        {
            
           a.transform.position = a.transform.position - direction * asteroidSpeed * Time.deltaTime;

            if (a.col.IsTouching(playerCollider))
            {
                //We hit a thing!!!
                //TODO hook up to damage the ship health
                //@RICHARD-LEE
            }

            if (!a.col.IsTouching(playArea))
            {
                //Remove the asteroid
                indexToRemove.Add(a);
                currentNumberOfAsteroids--;
            }

            if (a.col.IsTouching(asteroidSpawnArea))
            {
                numberInSpawnArea++;
            }

            index++;
        }

        foreach(Asteroid theRemoved in indexToRemove)
        {
            asteroids.Remove(theRemoved);
            Destroy(theRemoved.gameObject);
        }

        if(numberInSpawnArea < 4)
        {

            if (currentNumberOfAsteroids < maxNumberOfAsteroids)
            {
                float xPos = UnityEngine.Random.Range(asteroidSpawnArea.offset.x - asteroidSpawnArea.size.x / 2, asteroidSpawnArea.offset.x + asteroidSpawnArea.size.x / 2);
                float yPos = UnityEngine.Random.Range(asteroidSpawnArea.offset.y - asteroidSpawnArea.size.y / 2, asteroidSpawnArea.offset.y + asteroidSpawnArea.size.y / 2);

                asteroids.Add(Instantiate(asteroidPrefab, new UnityEngine.Vector3(xPos, yPos, 0), new UnityEngine.Quaternion()).GetComponent<Asteroid>());
                currentNumberOfAsteroids++;
            }
        }


    }

}