using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using Object = System.Object;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ReactorCoreSystem : MonoBehaviour {
    
    private const int rows = 5;
    private const int cols = 7;
    private const int gridWidth = 80;
    private const int gridHeight = 70;
    private const float sideScrollBoundsLimit = 3.2f; // +/- for right and left respectively

    [Header("Scrolling System")] [SerializeField][Range(0.0f, 2.0f)]
    private float m_scrollSpeed = 0.5f;
    
    [Header("ReactorCore Prefab")] [SerializeField]
    private GameObject reactorCorePrefab;

    [SerializeField] private Transform columnSpawnTransform;
    [SerializeField] private List<GameObject> columnList;
    
    private void Awake() {
        _initGridSystem();
    }

    private void Update() {
        _gridScrollingSystem();
    }
    
    private void _initGridSystem() {
        var colSpawnPosition = columnSpawnTransform.position;
        columnList = new List<GameObject>(cols);
        for (int i = -1; i <= cols; i++) { // Buffer columns for infinite scrolling
            var rowSpawnPos = new Vector3(colSpawnPosition.x + (i * (0.8f)),colSpawnPosition.y, colSpawnPosition.z);
            columnList.Add(_createColumn(rowSpawnPos));
        }
        // scope into a system without repeating random numbers
        foreach (var col in columnList) {
            _assignRandomCoreEmpty(col.transform);
        }
    }

    private void _gridScrollingSystem() {
        for (int i = 0; i < columnList.Count; i++) {
            var column = columnList[i];
            _scrollColumn(column.transform);
        }
    }

    private void _scrollColumn(Transform columnParent) {
        var position = columnParent.position;
        position = new Vector3(position.x + (Time.deltaTime * m_scrollSpeed), position.y,
            position.z);
        columnParent.position = position;

        // replacement system
        if (columnParent.position.x > sideScrollBoundsLimit + 0.8f) {
            var colPos = columnParent.position;
            colPos = new Vector3( -(sideScrollBoundsLimit), colPos.y,
                colPos.z);
            columnParent.position = colPos;
        }
    }

    private GameObject _createColumn(Vector3 columnSpawnPosition) {
        // create an empty parent at columnSpawnTransform
        var columnParent = new GameObject("ColumnParent");
        columnParent.transform.parent = this.transform;
        columnParent.transform.position = columnSpawnPosition;
        
        List<GameObject> column = new List<GameObject>(rows);
        for (int i = 0; i < rows; i++) {
            var spawnPos = new Vector3(columnSpawnPosition.x, columnSpawnPosition.y - (i * (0.7f)), columnSpawnPosition.z);
            var core = Instantiate(reactorCorePrefab,spawnPos, Quaternion.identity, columnParent.transform);
        }
        return columnParent;
    }

    private void _assignRandomCoreEmpty(Transform columnParent) {
        int randomNum = UnityEngine.Random.Range(0, rows);
        for(int i = 0 ; i < rows; i++) {
            var reactorCoreObj = columnParent.GetChild(i);
            if (i == randomNum) { 
                reactorCoreObj.GetComponent<ReactorCore>().MIsEmpty = true;
            }
        }
    }
}
