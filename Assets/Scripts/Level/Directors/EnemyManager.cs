using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject bluePrint;

    private Vector3Int[] path;
    public List<GameObject> activeTanks = new List<GameObject>();
    private Queue<string> inactiveTanks = new Queue<string>();
    private Queue<int> delays = new Queue<int>();
    private Dictionary<string, GameObject> enemyMap = new Dictionary<string, GameObject>();

    private int turnCounter;
    private int turnsSinceLastSpawn;
    private List<GameObject> toDestroy = new List<GameObject>();
    private GameObject bm;

    public Vector3Int[] Path { get => path; set => path = value; }

    private void Start()
    {
        bm = GameObject.Find("Director/BaseManager");

        turnCounter = 0;

        //simple enemy map
        enemyMap.Add("tank", bluePrint);

        //sample tank queue
        for(int i = 0; i < 15; i++)
        {
            inactiveTanks.Enqueue("tank");
            delays.Enqueue(2);
        }

        //simple path
        path = new Vector3Int[] { new Vector3Int(14, 5, 0),
                                    new Vector3Int(14, 4, 0),
                                    new Vector3Int(13, 3, 0),
                                    new Vector3Int(13, 2, 0),
                                    new Vector3Int(12, 1, 0),
                                    new Vector3Int(12, 0, 0),
                                    new Vector3Int(11, 0, 0),
                                    new Vector3Int(10, 0, 0),
                                    new Vector3Int(9, 0, 0),
                                    new Vector3Int(8, -1, 0),
                                    new Vector3Int(7, -1, 0),
                                    new Vector3Int(7, 0, 0),
                                    new Vector3Int(6, 0, 0),
                                    new Vector3Int(5, 1, 0),
                                    new Vector3Int(6, 2, 0),
                                    new Vector3Int(7, 2, 0),
                                    new Vector3Int(8, 2, 0),
                                    new Vector3Int(9, 2, 0),
                                    new Vector3Int(10, 2, 0),
                                    new Vector3Int(10, 3, 0),
                                    new Vector3Int(11, 4, 0),
                                    new Vector3Int(11, 5, 0),
                                    new Vector3Int(11, 6, 0),
                                    new Vector3Int(10, 6, 0),
                                    new Vector3Int(9, 6, 0),
                                    new Vector3Int(8, 6, 0),
                                    new Vector3Int(7, 6, 0),
                                    new Vector3Int(6, 6, 0),
                                    new Vector3Int(5, 5, 0),
                                    new Vector3Int(4, 5, 0),
                                    new Vector3Int(4, 4, 0),
                                    new Vector3Int(3, 4, 0),
                                    new Vector3Int(2, 4, 0),
                                    new Vector3Int(1, 3, 0),
                                    new Vector3Int(2, 2, 0),
                                    new Vector3Int(3, 2, 0),
                                    new Vector3Int(3, 1, 0),
                                    new Vector3Int(3, 0, 0),
                                    new Vector3Int(2, 0, 0),
                                    new Vector3Int(1, 0, 0),
                                    new Vector3Int(0, 1, 0),
                                    new Vector3Int(0, 2, 0),
                                    new Vector3Int(-1, 3, 0),
                                    new Vector3Int(-2, 3, 0)
        };

    }

    private void Update()
    {
        
    }

    private void spawnEnemies()
    {
        if (delays.Count > 0)
        {
            int nextSpawn = delays.Dequeue();
            if (nextSpawn <= turnsSinceLastSpawn)
            {
                turnsSinceLastSpawn = 0;
                GameObject enemyToAdd = enemyMap[inactiveTanks.Dequeue()];
                activeTanks.Add(Instantiate(enemyToAdd, FindObjectOfType<Grids>().GetCellCenterWorld(path[0]), Quaternion.identity));
                enemyToAdd.GetComponent<Tank>().currentPosition = path[0];
            }
            else
            {
                delays.Enqueue(nextSpawn);
            }
        }
        else
        {
            Debug.Log("Enemy queue empty!");
        }
    }

    public void nextTurn()
    {
        turnCounter++;
        turnsSinceLastSpawn++;
        if (activeTanks.Count > 0)
        {
            for (int i = activeTanks.Count - 1; i >= 0; i--)
            {
                //tank i is dead
                if(activeTanks[i].GetComponent<Tank>().hp <= 0)
                {
                    GameObject toDestroy = activeTanks[i];
                    activeTanks.RemoveAt(i);
                    Object.Destroy(toDestroy);
                }
                else
                {
                    //move tank along path
                    int currentPathCount = activeTanks[i].GetComponent<Tank>().pathCount++;
                    if (currentPathCount < path.Length)
                    {

                        activeTanks[i].gameObject.transform.position = FindObjectOfType<Grids>().GetCellCenterWorld(path[currentPathCount]);
                        activeTanks[i].gameObject.GetComponent<Tank>().currentPosition = path[currentPathCount];
                    }
                    else
                    {
                        Debug.Log("Tank has reach end of path.");
                        bm.GetComponent<BaseManager>().TakeDamage(activeTanks[i].GetComponent<Tank>().dmg);
                        Object.Destroy(activeTanks[i]);
                        activeTanks.RemoveAt(i);

                    }
                }
            }
        }
        spawnEnemies();
    }
}
