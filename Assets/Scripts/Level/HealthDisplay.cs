using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Text healthText;

    private GameObject bm;

    private void Start()
    {
        bm = GameObject.Find("Director/BaseManager");
    }
    void Update()
    {
        int baseHealth = bm.GetComponent<BaseManager>().health;
        healthText.text = "HEALTH: " + baseHealth;
    }
}
