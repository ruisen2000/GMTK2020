using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRod : MonoBehaviour {
    [SerializeField] private float speed = 0.5f;
    private bool shouldLaunch = false;

    private void Awake() {
        shouldLaunch = false;
    }
    
    public delegate void OnFuelRodInsert();
    public static event OnFuelRodInsert OnFuelRodInsertEvent;
    private void OnEnable() {
        LevelInputHandler.OnLeverReleased += LaunchFuelRod;
        OnFuelRodInsertEvent += DestroythisRod;
    }
    
    private void OnDisable() {
        LevelInputHandler.OnLeverReleased -= LaunchFuelRod;
        OnFuelRodInsertEvent -= DestroythisRod;

    }

    private void DestroythisRod() {
        gameObject.SetActive(false);
    }

    private void LaunchFuelRod(float value) {
        shouldLaunch = true;
    }

    private bool _customColliderCheck() {
        float checkLength = 1F;

        int collidersHitCount = 0;
        for (int i = -3; i <=3; i++) {
            var position = new Vector3((i * 0.1f) + transform.position.x, transform.position.y, transform.position.z);
            Debug.DrawLine(position, new Vector3(position.x, position.y + checkLength, position.z), Color.cyan, Time.deltaTime,
                false);
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
                OnFuelRodInsertEvent();
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
