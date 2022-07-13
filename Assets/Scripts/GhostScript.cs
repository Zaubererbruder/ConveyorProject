using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private bool _constructable;
    private List<Collider> _collisions = new List<Collider>();
    private Transform _transform;
    public GameObject Prefab => _prefab;
    public Grid Grid { get; set; }

    private void Awake()
    {
        _transform = transform;
        SetPrefab(_prefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        var blockInfo = other.GetComponent<BlockInfo>();
        if (blockInfo != null)
        {
            _collisions.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var blockInfo = other.GetComponent<BlockInfo>();
        if (blockInfo != null)
        {
            _collisions.Remove(other);
        }
    }

    public void SetPrefab(GameObject newPrefab)
    {
        _prefab = newPrefab;
        var _mesh = GetComponent<MeshFilter>();
        var _prefabMesh = _prefab.GetComponentInChildren<MeshFilter>();
        _mesh.mesh = _prefabMesh.sharedMesh;
        var newPrefabTransform = newPrefab.transform;
        _transform.localScale = newPrefabTransform.localScale;
        _transform.rotation = newPrefabTransform.rotation;
    }

    public void ConstructPrefab()
    {
        if (_collisions.Count > 0)
        {
            Debug.Log("Construction Forbidden");
            return;
        }
        var obj = Instantiate(_prefab, _transform.position, _transform.rotation);
        var comp = obj.GetComponent<BlockInfo>();
        comp.Grid = Grid;
        comp.GridPoint = Grid.WorldToCell(obj.transform.position);
    }
}
