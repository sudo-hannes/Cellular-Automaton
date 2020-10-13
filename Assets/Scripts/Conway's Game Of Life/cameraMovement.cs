using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Zoom();
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.up * zInput + transform.right * xInput;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float z = cam.transform.position.z;

        if (z < -241)
        {
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, -241.0f);
            return;
        }
        if (z > -53)
        {
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, -53.0f);
            return;
        }
        cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
    }

    public void FocusOnPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
