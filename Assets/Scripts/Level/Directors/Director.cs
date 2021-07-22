using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    public EnemyManager em;
    public TowerManager tm;
    public Tilemap tilemap;
    public string gameState;
    public GameObject gameOverScreen;
    public GameObject firingScreen;
    public GameObject movingScreen;

    public Button nextTurnButton;

    public float fireWait;
    public float moveWait;

    private float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
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
            default:
                Debug.Log("ERROR: game state not found");
                break;
        }
        handleTurn();
    }

    private void onMouseClickEvent(Vector3 point)
    {
        point.z = 0;
        //Debug.Log("Click detected at world point: " + worldPoint);
        Debug.Log("Click detected on cell: " + tilemap.WorldToCell(point));
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

}
