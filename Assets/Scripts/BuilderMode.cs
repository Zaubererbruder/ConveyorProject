using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMode : MonoBehaviour
{
    [SerializeField] private GhostScript _ghostPrefab;
    [SerializeField] private GridSystem _gridSystem;
    [SerializeField] private Cursor _cursor;
    [SerializeField] private List<GameObject> _prefabsForBuild;

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
            var blockInfo = collidedObject.GetComponent<BlockInfo>();
            var cellGridNear = blockInfo.Grid.WorldToCell(_cursor.CorrectHitPoint);
            var direction = Normilize(cellGridNear - blockInfo.GridPoint);
            var correctCell = cellGridNear + ((_ghost.Prefab.GetComponent<BlockInfo>().Size - Vector3Int.one) * direction / 2);
            var newpos = blockInfo.Grid.GetCellCenterWorld(correctCell);
            _ghostTransform.position = new Vector3(newpos.x, newpos.y, newpos.z);
            _ghost.Grid = blockInfo.Grid;
        }
        else
        {
            _ghostTransform.position = _cursor.Hit.point;
            _ghost.Grid = null;
        }
    }

    public Vector3Int Normilize(Vector3Int vect)
    {
        var newVect = vect;
        while (newVect.magnitude > 1)
        {
            if (Mathf.Abs(newVect.x) > 0)
                newVect.x -= (int)Mathf.Sign(newVect.x);
            if (Mathf.Abs(newVect.y) > 0)
                newVect.y -= (int)Mathf.Sign(newVect.y);
            if (Mathf.Abs(newVect.z) > 0)
                newVect.z -= (int)Mathf.Sign(newVect.z);
        }
        return newVect;
    }

    public void ConstructBlock()
    {
        if (_ghost.Grid == null)
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

    public void Rotate(float x, float y, float z)
    {
        _ghostTransform.Rotate(x, y, z);
    }

    public void ChangeBuilding()
    {
        var index = _prefabsForBuild.IndexOf(_ghost.Prefab);
        if (index + 1 == _prefabsForBuild.Count)
            _ghost.SetPrefab(_prefabsForBuild[0]);
        else
            _ghost.SetPrefab(_prefabsForBuild[index+1]);
    }
}
public enum Layer
{
    Blocks = 10
}