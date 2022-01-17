using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    public float zoomFactor;

    [SerializeField]
    float zoomSpeed;

    [SerializeField]
    float zoomChunk;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float cameraClamp;

    private float originalSize = 0f;

    private Camera thisCamera;

    // Use this for initialization
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        originalSize = thisCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        SetZoom();
        moveCamera();
    }

    void moveCamera()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (!movement.Equals(Vector3.zero))
            {
            Vector3 unclamped = transform.position + (movement * moveSpeed * zoomFactor);

            unclamped.x = Math.Min(Math.Max(unclamped.x, -cameraClamp), cameraClamp);
            unclamped.y = Math.Min(Math.Max(unclamped.y, -cameraClamp), cameraClamp);
            unclamped.z = -10;

            transform.position = unclamped;
        }
    }

    void SetZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (zoomFactor > 0.25f)
            {
                zoomFactor -= zoomChunk;
            }
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            if (zoomFactor < 1.25f)
            {
                zoomFactor += zoomChunk;
            }
        }

        float targetSize = originalSize * zoomFactor;
        if (targetSize != thisCamera.orthographicSize)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize,
    targetSize, Time.deltaTime * zoomSpeed);
        }  
    }
}