using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class SimpleTower : MonoBehaviour
{
    public Vector3Int gridLoc;
    public int range;
    public int dmg;

    public GameObject projectile;
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
    }

    public bool PlaceTurret(Vector3Int newPosition)
    {
        gridLoc = newPosition;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 target)
    {

        GameObject newProjectile = Instantiate(projectile, spawn.position, Quaternion.identity);
        newProjectile.GetComponent<Arrow>().target = target;
        newProjectile.GetComponent<Arrow>().fireTime = 0;
    }
}
