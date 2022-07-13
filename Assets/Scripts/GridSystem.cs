using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private Dictionary<Grid, List<GameObject>> _gridsObjects = new Dictionary<Grid, List<GameObject>>();

    public GameObject AddGrid(Vector3 position)
    {
        var emptyObject = new GameObject("Grid");
        var gridTransform = emptyObject.transform;
        gridTransform.SetParent(transform);
        gridTransform.position = position - Vector3.one * 1.5f;
        var grid = emptyObject.AddComponent<Grid>();
        grid.cellSize = Vector3.one*3;

        _gridsObjects.Add(grid, new List<GameObject>());

        return emptyObject;
    }

    public void Add(Grid grid, GameObject obj)
    {
        _gridsObjects[grid].Add(obj);
    }
        
}
