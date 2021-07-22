using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public int speed;
    public int hp;
    public int dmg;
    
    public int pathCount;

    public Vector3Int currentPosition;

    private void Start()
    {
        pathCount = 1;
    }
    public void takeDamage(int dmg)
    {
        hp -= dmg;
    }
}
