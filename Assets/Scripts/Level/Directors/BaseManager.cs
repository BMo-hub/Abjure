using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        health = Math.Max( health - dmg, 0 );
        if(health <= 0)
        {
            transform.parent.GetComponent<Director>().gameState = "FINISHED_LOST";
        }
    }
}
