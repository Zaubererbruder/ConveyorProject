using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;

    public void SetPrefab(GameObject newPrefab)
    {
        _prefab = newPrefab;
        var _mesh = GetComponent<MeshFilter>();
        var _prefabMesh = _prefab.GetComponent<MeshFilter>();
        _mesh.mesh = _prefabMesh.mesh;
    }
}
