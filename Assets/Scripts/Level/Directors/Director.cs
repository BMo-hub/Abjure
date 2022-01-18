using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.IO;

public class Director : MonoBehaviour
{
    public EnemyManager em;
    public TowerManager tm;
    public string gameState;
    public GameObject gameOverScreen;
    public GameObject firingScreen;
    public GameObject movingScreen;

    public Grids g;

    public Button nextTurnButton;

    public float fireWait;
    public float moveWait;

    public TextAsset pathOutput;

    private float timeCounter;

    private List<Vector3> newPath;

    // Start is called before the first frame update
    void Start()
    {
        newPath = new List<Vector3>();
        gameState = "RUNNING";
        timeCounter = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            //player is acting, game is running normally
            case "RUNNING":
                movingScreen.GetComponent<ScreenDisplay>().turnOff();
                firingScreen.GetComponent<ScreenDisplay>().turnOff();
                if (Input.GetMouseButtonDown(0))
                {
                    onMouseClickEvent(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                break;
            //the player has lost. Display the game over screen
            case "FINISHED_LOST":
                gameOverScreen.GetComponent<GameOverDisplay>().Setup();
                break;
            //the turrets are actively firing. PLayer has no control.
            case "FIRING":
                movingScreen.GetComponent<ScreenDisplay>().turnOff();
                firingScreen.GetComponent<ScreenDisplay>().turnOn();
                break;
            //the enemies are actively moving. Player has no control.
            case "MOVING":
                firingScreen.GetComponent<ScreenDisplay>().turnOff();
                movingScreen.GetComponent<ScreenDisplay>().turnOn();
                break;
            case "WRITING_PATH":
                if (Input.GetMouseButtonDown(0))
                {
                    addToPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                if (Input.GetButtonDown("Jump"))
                {
                    writePath();
                }
                break;
        }
        handleTurn();
    }

    private void onMouseClickEvent(Vector3 point)
    {
        point.z = 0;
        Debug.Log("Click detected on cell: " + g.WorldToCell(point));
        //addTower(point);

    }

    private void handleTurn()
    {
        switch (gameState)
        {
            //player is acting, game is running normally
            case "RUNNING":
                break;
            //the player has lost. Display the game over screen
            case "FINISHED_LOST":
                break;
            //the turrets are actively firing. PLayer has no control.
            case "FIRING":
                //if time since we started firing > fireWait, finish firing and switch to moving
                //and reset time
                if (Time.timeSinceLevelLoad - timeCounter > fireWait)
                {
                    tm.nextTurn();
                    gameState = "MOVING";
                    timeCounter = Time.timeSinceLevelLoad;
                }
                break;
            //the enemies are actively moving. Player has no control.
            case "MOVING":
                if (Time.timeSinceLevelLoad - timeCounter > fireWait)
                {
                    em.nextTurn();
                    gameState = "RUNNING";
                    timeCounter = Time.timeSinceLevelLoad;
                    nextTurnButton.interactable = true;
                }
                break;
            default:
                Debug.Log("ERROR: game state not found");
                break;
        }
    }

    public void nextTurn()
    {
        if (gameState == "RUNNING")
        {
            gameState = "FIRING";
            timeCounter = Time.timeSinceLevelLoad;
            nextTurnButton.interactable = false;
        }
    }
    public bool addTowerWorld(Vector3 worldPosition)
    {
        return tm.addTower(g.WorldToCell(worldPosition));
    }

    private void addToPath( Vector3 p )
    {
        newPath.Add(g.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }

    private void writePath()
    {
        string filePath = "Assets/Debug/path.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, false);
        foreach(Vector3 v in newPath)
        {
            writer.WriteLine(v);
        }
        writer.Close();
    }
}
