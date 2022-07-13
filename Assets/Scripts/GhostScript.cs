using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private Transform _transform;
    public GameObject Prefab => _prefab;
    public Grid Grid { get; set; }

    private void Awake()
    {
        _transform = transform;
        SetPrefab(_prefab);
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
        var obj = Instantiate(_prefab, _transform.position, _transform.rotation);
        var comp = obj.GetComponent<BlockInfo>();
        comp.Grid = Grid;
        comp.GridPoint = Grid.WorldToCell(obj.transform.position);
    }
}
