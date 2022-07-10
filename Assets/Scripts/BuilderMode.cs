using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMode : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] private GhostScript _ghost;
    [SerializeField] private LayerMask _filterMask;
    [SerializeField] private GridSystem _gridSystem;

    private Transform _ghostTransform;

    void Start()
    {
        var obj = GameObject.Instantiate(_ghost);
        _ghostTransform = obj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(transform.position, ray.direction, out hit, 1000, _filterMask);
        if(hit.collider == null)
        {
            return;
        }
        var collidedObject = hit.collider.gameObject;
        var collidedWithGridBlock = collidedObject.TryGetComponent<GridBlock>(out var gridBlock);
        if (collidedWithGridBlock)
        {
            var hitnew = hit.point - ray.direction*0.001f;
            var newpos = gridBlock.Grid.GetCellCenterWorld(gridBlock.Grid.WorldToCell(hitnew));
            _ghostTransform.position = new Vector3(newpos.x, newpos.y, newpos.z);
        }
        else
        {
            _ghostTransform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        if(Input.GetMouseButtonDown(0))
        {
            var obj = Instantiate(_ghost.Prefab, _ghostTransform.position, Quaternion.identity);

            if (!collidedWithGridBlock)
            {
                var gridObject = _gridSystem.AddGrid(obj.transform.position);
                var comp = obj.GetComponent<GridBlock>();
                comp.Grid = gridObject.GetComponent<Grid>();
                comp.GridPoint = comp.Grid.WorldToCell(obj.transform.position);
            }
            else
            {
                var gridblock = hit.collider.gameObject.GetComponent<GridBlock>();
                var comp = obj.GetComponent<GridBlock>();
                comp.Grid = gridblock.Grid;
                comp.GridPoint = comp.Grid.WorldToCell(obj.transform.position);
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(collidedObject);
        }
    }


    
}
