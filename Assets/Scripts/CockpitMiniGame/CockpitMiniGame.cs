using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CockpitMiniGame : Minigame
{



    LineRenderer path;
    List<Asteroid> asteroids;

    [SerializeReference]
    BoxCollider2D asteroidPlayArea;

    [SerializeReference]
    BoxCollider2D asteroidSpawnArea;

    [SerializeReference]
    BoxCollider2D playerClickArea;

    [SerializeReference]
    CircleCollider2D playerCollider;

    [SerializeReference]
    GameObject asteroidPrefab;

    [SerializeReference]
    float asteroidSpeed = 1.0f;


    [SerializeReference]
    int maxNumberOfAsteroids = 10;

    int currentNumberOfAsteroids = 0;

    UnityEngine.Vector2 moveDirection;

    List<UnityEngine.Vector3> wayPoints;
    UnityEngine.Vector3 nextPoint;
    bool nextPointSet;

    public float distanceTravelTotal;

    [SerializeReference]
    PulldownMenuController pullDownMenu;

    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<LineRenderer>();
        asteroids = new List<Asteroid>();
        wayPoints = new List<UnityEngine.Vector3>();
        nextPointSet = false;
        moveDirection.x = 0;
        moveDirection.y = 1;
        distanceTravelTotal = 0.0f;
        DataHolder.allData.shipHealth = 1.0f;
    }


    // Update is called once per frame
    void Update()
    {


        if (nextPointSet)
        {
            moveDirection = nextPoint - playerCollider.gameObject.transform.position;
            moveDirection.Normalize();
            
            if(moveDirection.y < 0)
            {
                moveDirection *= -1;
            }

            if(moveDirection.y < 0.01f)
            {
                moveDirection.y = 0.4f;
            }

        }

        updateAsteroids();
        updatePath();

        distanceTravelTotal += moveDirection.y * Time.deltaTime;
        
        pullDownMenu.UpdateDistanceTravelled(distanceTravelTotal);

        asteroidSpeed += asteroidSpeed * 0.01f * Time.deltaTime ;
    }

    private void updatePath()
    {
        

        if(Input.GetMouseButtonDown(0) && playerClickArea.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && !pullDownMenu.menuDown)
        {
            //Add to the end of the list

            UnityEngine.Vector2 proposedNewSpot = Camera.main.ScreenToWorldPoint(Input.mousePosition);

         
            bool validSpotFound = true;
            foreach(UnityEngine.Vector2 point in wayPoints)
            {
                if(point.y > proposedNewSpot.y)
                {
                    validSpotFound = false;
                    break;
                }

            }

            if (validSpotFound)
            {
                AudioController.instance.Play("DialClick");

                mHealth = UnityEngine.Vector3.Distance(proposedNewSpot, playerCollider.gameObject.transform.position) ;
                wayPoints.Add(proposedNewSpot);
            }


        }
        else
        {

            List<UnityEngine.Vector2> spotsToRemove = new List<UnityEngine.Vector2>();


            nextPoint = nextPoint - (UnityEngine.Vector3)moveDirection * Time.deltaTime;
            mHealth = mHealth - moveDirection.y * asteroidSpeed * Time.deltaTime;
           
            for (int i = 0; i < wayPoints.Count;i++)
            {

                wayPoints[i] = wayPoints[i] - (UnityEngine.Vector3)moveDirection * asteroidSpeed * Time.deltaTime;

                if (wayPoints[i].y <= playerCollider.gameObject.transform.position.y)
                {
                    spotsToRemove.Add(wayPoints[i]);
                }
        
           
            }

            if(wayPoints.Count > 0)
            {
                nextPoint = wayPoints[0];
                nextPointSet = true;
            }
            else
            {
                nextPointSet = false;
                mHealth = 0.0f;
            }


            foreach (UnityEngine.Vector2 toRemove in spotsToRemove)
            {
                wayPoints.Remove(toRemove);
            }

          

            path.positionCount = wayPoints.Count + 1;
            path.SetPosition(0, playerCollider.gameObject.transform.position);

            for (int i = 0; i < wayPoints.Count; i++)
            {
                path.SetPosition(i+1, new UnityEngine.Vector3(wayPoints[i].x,wayPoints[i].y,-0.5f));
            }

        }

    }

    private void updateAsteroids()
    {


        List<Asteroid> indexToRemove = new List<Asteroid>();

        int numberInSpawnArea = 0;
        int index = 0;
        //Find the current path point
        //Move all asteroids opposide to that direction (ie towards
        foreach(Asteroid a in asteroids)
        {
            
           a.transform.position = (UnityEngine.Vector2)a.transform.position - moveDirection * asteroidSpeed * Time.deltaTime;

            if (a.col.IsTouching(playerCollider))
            {
                //We hit a thing!!!
                //TODO hook up to damage the ship health
                //@RICHARD-LEE
                AudioController.instance.Play("BeepingTrimmed");

                DataHolder.allData.shipHealth -= 0.1f;
       
                //Also remove it
                indexToRemove.Add(a);
                currentNumberOfAsteroids--;
            }

            if (!a.col.IsTouching(asteroidPlayArea))
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