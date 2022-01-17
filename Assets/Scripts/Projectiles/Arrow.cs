using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float fireTime;
    public float speed;

    private Vector3 movePosition;
    private float nextX;
    private float nextY;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //look at target
        transform.right = target - transform.position;

        //if at target, destroy
        if ((transform.position - target).magnitude <= 0.1f)
        {
            Object.Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }

    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }
}
