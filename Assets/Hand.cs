using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lr.enabled)
        {
            Vector3 lineEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineEnd.z = 10;
            lr.SetPosition(1, lineEnd);
            Debug.Log("Showing line between points:\n" +
                lr.GetPosition(0) + "\n" +
                lr.GetPosition(1));
        }
    }

    public void showLine(Vector3 cardLocation)
    {
        lr.SetPosition(0, cardLocation);
        lr.enabled = true;
    }

    public void hideLine()
    {
        lr.enabled = false;
    }
}
