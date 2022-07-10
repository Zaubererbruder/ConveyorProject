using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private LayerMask _filterBuilderMask;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength = 1000f;

    private CursorMode _currentMode = CursorMode.Builder;
    private Ray _ray;
    private RaycastHit _hit;

    public CursorMode CurrentMode => _currentMode;
    public RaycastHit Hit => _hit;
    public bool HasHit => _hit.collider != null;
    public GameObject CollidedObject => _hit.collider.gameObject;
    public Vector3 CorrectHitPoint => _hit.point - _ray.direction * 0.001f;

    private void Update()
    {
        if (_currentMode == CursorMode.Default)
        {
            return;
        }

        if (_currentMode == CursorMode.Builder)
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(transform.position, _ray.direction, out _hit, _rayLength, _filterBuilderMask);
            return;
        }
    }
}

public enum CursorMode
{
    Default,
    Builder
}