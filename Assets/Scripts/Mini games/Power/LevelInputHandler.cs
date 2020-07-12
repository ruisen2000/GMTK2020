using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class LevelInputHandler : MonoBehaviour {

    [SerializeField] [ReadOnly] private Transform startPos, endPos;
    private float dist;

    private Vector3 startPosition, endPosition;
    
    private bool m_isLeverDragged = false;
    private bool m_shouldTriggerRelease = false;
    private Camera _camera;
    
    public delegate void OnLeverReleasedEvent(float value);
    public static event OnLeverReleasedEvent OnLeverReleased;

    private void OnEnable() {
        _handleLeverMovement(); // set initial pos
    }

    private void Awake() {
        _camera = Camera.main;
        startPosition = transform.TransformPoint(startPos.position);        
        endPosition = transform.TransformPoint(endPos.position);        
        dist = Vector3.Distance(endPosition, startPosition);
    }

    private void OnMouseDrag() {
        m_isLeverDragged = true;
    }
    
    private void OnMouseUp() {
        m_isLeverDragged = false;
        m_shouldTriggerRelease = true;
    }

    private void _handleLeverMovement() {
        if (!m_isLeverDragged) {
            // HACK: PLS FORGIVE
            if (!m_shouldTriggerRelease) return;
            transform.position = startPos.position;
            // trigger event to send fuelrod
            if (OnLeverReleased != null) {
                OnLeverReleased(1);
                m_shouldTriggerRelease = false;
            }
            return;
        }
        
        var position = transform.position;
        float zDepth = position.z - _camera.transform.position.z;
        var currentMouseWorldPos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,zDepth));
        var newLeverPos = new Vector3(position.x, currentMouseWorldPos.y, position.x);
        position = newLeverPos;
        transform.position = position;
        
        //clamping
        if (position.y >= endPos.position.y) {
            transform.position = endPos.position;
        }
        if (position.y <= startPos.position.y) {
            transform.position = startPos.position;
        }
    }

    private void Update() {
        _handleLeverMovement();
    }


}
