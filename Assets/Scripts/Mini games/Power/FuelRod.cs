﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuelRod : MonoBehaviour {
    [SerializeField] private float speed = 0.5f;
    private bool shouldLaunch = false;

    [SerializeField] private Transform outOfBoundsT;
    public delegate void OnFuelRodInsert(bool success);
    public static event OnFuelRodInsert OnFuelRodInsertEvent;

    private void Awake() {
        outOfBoundsT = GameObject.FindGameObjectWithTag("OutOfBounds").transform;
    }

    private void Start() {
        shouldLaunch = false;

    }
    
    public void ChangeHandlersOrdering()
    {
        if (OnFuelRodInsertEvent != null) {
            var invocationList = OnFuelRodInsertEvent.GetInvocationList().OfType<OnFuelRodInsert>().ToList();

            foreach (var handler in invocationList)
            {
                OnFuelRodInsertEvent -= handler;
            }

            //Change ordering now, for example in reverese order as follows
            for (int i = invocationList.Count - 1; i >= 0; i--)
            {
                OnFuelRodInsertEvent += invocationList[i];
            }
        }
    }
    

    private void OnEnable() {
        LevelInputHandler.OnLeverReleased += LaunchFuelRod;
        OnFuelRodInsertEvent += DestroythisRod;
        ChangeHandlersOrdering();
    }
    
    private void OnDisable() {
        LevelInputHandler.OnLeverReleased -= LaunchFuelRod;
        OnFuelRodInsertEvent -= DestroythisRod;
    }

    private void DestroythisRod(bool success) {
        Destroy(this.gameObject, 0);
    }

    private void LaunchFuelRod(float value) {
        shouldLaunch = true;
    }

    private bool _customColliderCheck() {
        float checkLength = 1F;

        int collidersHitCount = 0;
        for (int i = -3; i <=3; i++) {
            var position = new Vector3((i * 0.1f) + transform.position.x, transform.position.y, transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, checkLength);
            if (hit.collider != null) {
                collidersHitCount++;
            }
            if (collidersHitCount >= 6) {
                hit.collider.gameObject.GetComponent<ReactorCore>().MIsEmpty = false;
                return true;
            }
        }

        return false;
    }
    void FixedUpdate() {
        if (_customColliderCheck()) {
            if (OnFuelRodInsertEvent != null) {
                OnFuelRodInsertEvent(true);
            }
        }
        if (transform.position.y >= outOfBoundsT.transform.position.y) {
            if (OnFuelRodInsertEvent != null) {
                OnFuelRodInsertEvent(false);
            }
        }
    }
    private void Update() {
        // move forward.
        _moveFuelRod();
    }
    
    private void _moveFuelRod() {
        if (!shouldLaunch) return;
        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
    }
}
