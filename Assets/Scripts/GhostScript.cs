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
        var _prefabMesh = _prefab.GetComponent<MeshFilter>();
        _mesh.mesh = _prefabMesh.sharedMesh;
    }

    public void ConstructPrefab()
    {
        var obj = Instantiate(_prefab, _transform.position, Quaternion.identity);
        var comp = obj.GetComponent<GridBlock>();
        comp.Grid = Grid;
        comp.GridPoint = Grid.WorldToCell(obj.transform.position);
    }
}
