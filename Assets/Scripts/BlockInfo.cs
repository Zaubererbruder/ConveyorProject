using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    [SerializeField] private Vector3Int _size;
    public Grid Grid { get; set; }
    public Vector3Int GridPoint { get; set; }
    public Vector3Int Size => _size;

    private void Awake()
    {
        
    }
}
