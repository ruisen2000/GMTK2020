using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using Object = System.Object;

public class ReactorCoreSystem : MonoBehaviour {
    
    private const int rows = 5;
    private const int cols = 7;
    private const int gridWidth = 80;
    private const int gridHeight = 70;

    [Header("ReactorCore Prefab")] [SerializeField]
    private GameObject reactorCorePrefab;

    [SerializeField] private Transform columnSpawnTransform;
    [SerializeField] private List<List<GameObject>> grid;
    
    private void Awake() {
        _initGridSystem();
    }
    
    private void _initGridSystem() {
        var colSpawnPosition = columnSpawnTransform.position;
        grid = new List<List<GameObject>>(cols);
        for (int i = 0; i < cols; i++) {
            var rowSpawnPos = new Vector3(colSpawnPosition.x + ( i * (gridWidth/100.0f)),colSpawnPosition.y, colSpawnPosition.z);
            grid.Add(_createColumn(rowSpawnPos));
        }
    }

    private List<GameObject> _createColumn(Vector3 columnSpawnPosition) {
        // create an empty parent at columnSpawnTransform
        var columnParent = new GameObject("ColumnParent");
        columnParent.transform.parent = this.transform;
        columnParent.transform.position = columnSpawnPosition;
        
        List<GameObject> column = new List<GameObject>(rows);
        for (int i = 0; i < rows; i++) {
            var spawnPos = new Vector3(columnSpawnPosition.x, columnSpawnPosition.y - (i * (gridHeight/100.0f)), columnSpawnPosition.z);
            var core = Instantiate(reactorCorePrefab,spawnPos, Quaternion.identity, columnParent.transform);
            column.Add(core);
        }
        return column;
    }


}
