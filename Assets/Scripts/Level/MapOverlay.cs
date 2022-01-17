using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapOverlay : MonoBehaviour
{
    public bool showGrid;
    public Sprite border;
    public Canvas gridOverlay;

    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tile hoverTile = null;

    private Vector3Int previousMousePos = new Vector3Int();

    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
