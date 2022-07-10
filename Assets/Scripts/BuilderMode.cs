using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMode : MonoBehaviour
{
    [SerializeField] private GhostScript _ghostPrefab;
    [SerializeField] private GridSystem _gridSystem;
    [SerializeField] private Cursor _cursor;

    private GhostScript _ghost;
    private Transform _ghostTransform;

    void Awake()
    {
        _ghost = GameObject.Instantiate(_ghostPrefab);
        _ghostTransform = _ghost.transform;
    }

    void Update()
    {
        if(!_cursor.HasHit)
        {
            return;
        }

        var collidedObject = _cursor.CollidedObject;
        var collidedWithGridBlock = collidedObject.layer == (int)Layer.Blocks;
        if (collidedWithGridBlock)
        {
            var gridBlock = collidedObject.GetComponent<GridBlock>();
            var newpos = gridBlock.Grid.GetCellCenterWorld(gridBlock.Grid.WorldToCell(_cursor.CorrectHitPoint));
            _ghostTransform.position = new Vector3(newpos.x, newpos.y, newpos.z);
            _ghost.Grid = gridBlock.Grid;
        }
        else
        {
            _ghostTransform.position = _cursor.Hit.point;
            _ghost.Grid = null;
        }
    }

    public void ConstructBlock()
    {
        if(_ghost.Grid == null)
        {
            _ghost.Grid = _gridSystem.AddGrid(_ghostTransform.position).GetComponent<Grid>();
        }
        _ghost.ConstructPrefab();
    }

    public void DestroyBlock()
    {
        if(_cursor.HasHit && _cursor.CollidedObject.layer == (int)Layer.Blocks)
        {
            Destroy(_cursor.CollidedObject);
        }
    }

    public enum Layer
    {
        Blocks = 10
    }
}
