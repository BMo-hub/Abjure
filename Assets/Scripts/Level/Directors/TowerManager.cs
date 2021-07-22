using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Grid grid;
    public GameObject blueprint;
    public GameObject em;

    public List<GameObject> activeTowers;

    private List<GameObject> targets;
    // Start is called before the first frame update
    private LineRenderer l;

    void Start()
    {
        activeTowers = new List<GameObject>();
        targets = new List<GameObject>();
        l = gameObject.AddComponent<LineRenderer>();

        //sample tower
        Vector3Int newPosition = new Vector3Int(-4, 2, 0);
        GameObject newTower = Instantiate(blueprint, grid.GetCellCenterWorld(new Vector3Int(100, 100, 0)), Quaternion.identity);
        newTower.GetComponent<SimpleTower>().PlaceTurret(newPosition);
        newTower.transform.position = grid.CellToWorld(newPosition);
        activeTowers.Add(newTower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTower(Vector3Int gridLoc)
    {

    }

    public void nextTurn()
    {
        foreach (GameObject tower in activeTowers)
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

    public bool findTargets(GameObject tower)
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
}
