using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public Vector3 gridStart;
    public Vector3 gridEnd;

    public Grid bottomGrid;
    public Grid topGrid;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool worldPointInBounds(Vector3 p)
    {
        return gridPointInBounds( bottomGrid.WorldToCell( p ) );
    }

    public bool gridPointInBounds(Vector3 p)
    {
        return (p.x >= gridStart.x) && 
            (p.y >= gridStart.y) && 
            (p.x <= gridEnd.x) &&
            (p.y <= gridEnd.y);
    }

    public Vector3Int WorldToCell(Vector3 p)
    {
        return bottomGrid.WorldToCell(p);
    }

    public Vector3 CellToWorld(Vector3Int p)
    {
        return bottomGrid.CellToWorld(p);
    }

    public Vector3 GetCellCenterWorld(Vector3Int p)
    {
        return bottomGrid.GetCellCenterWorld(p);
    }

}
