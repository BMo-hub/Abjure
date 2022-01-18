using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public SimpleTower blueprint;
    public GameObject em;

    public List<SimpleTower> activeTowers;

    private List<GameObject> targets;

    void Start()
    {
        activeTowers = new List<SimpleTower>();
        targets = new List<GameObject>();
    }

    public void nextTurn()
    {
        foreach (SimpleTower tower in activeTowers)
        {
            findTargets(tower);
            if (targets.Count > 0)
            {
                //simple tower only damages the target closest to the end.
                GameObject mainTarget = targets[0];
                foreach (GameObject t in targets)
                {
                    if (t.GetComponent<Tank>().pathCount > mainTarget.GetComponent<Tank>().pathCount)
                    {
                        mainTarget = t;
                    }
                }

                //damage main target
                tower.GetComponent<SimpleTower>().Fire(mainTarget.transform.position);
                mainTarget.GetComponent<Tank>().takeDamage(tower.GetComponent<SimpleTower>().dmg);
            }
            targets.Clear();
        }
    }

    public bool findTargets(SimpleTower tower)
    {
        targets.Clear();
        bool found = false;

        Vector3Int gridLoc = tower.GetComponent<SimpleTower>().gridLoc;
        int range = tower.GetComponent<SimpleTower>().range;
        foreach (GameObject t in em.GetComponent<EnemyManager>().activeTanks)
        {
            for (int i = gridLoc.x - range; i <= gridLoc.x + range; i++)
            {
                for (int j = gridLoc.y - range; j <= gridLoc.y + range; j++)
                {
                    if (t.GetComponent<Tank>().currentPosition.Equals(new Vector3Int(i, j, 0)))
                    {
                        targets.Add(t);
                        found = true;
                        Debug.Log("Tank is in range!");
                    }
                }
            }
        }
        return found;
    }

    public bool addTower(Vector3Int gridLocation)
    {
        gridLocation.z = 0;
        if (!checkTowerValid(gridLocation))
        {
            return false;
        }

        Debug.Log("Placing tower at grid location: " + gridLocation);
        SimpleTower newTower = Instantiate(blueprint);
        newTower.GetComponent<SimpleTower>().PlaceTurret(gridLocation);
        newTower.transform.position = FindObjectOfType<Grids>().CellToWorld(gridLocation);
        activeTowers.Add(newTower);

        return true;
    }

    public bool checkTowerValid(Vector3Int gridLocation)
    {
        if (!FindObjectOfType<Grids>().gridPointInBounds(gridLocation))
        { 
            return false;
        }
        if (activeTowers.Count == 0 )
        {
            return true;
        }
        foreach (SimpleTower t in activeTowers)
        {
            if(gridLocation == t.gridLoc)
            {
                return false;
            }
        }
        return true;
    }
}
