using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Grid[] _grids;

    public GameObject AddGrid(Vector3 position)
    {
        var emptyObject = new GameObject("Grid");
        var gridTransform = emptyObject.transform;
        gridTransform.SetParent(transform);
        gridTransform.position = position - Vector3.one * 1.5f;
        var grid = emptyObject.AddComponent<Grid>();
        grid.cellSize = Vector3.one*3;
        
        return emptyObject;
    }
}
