using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;
    public float zoomBoundUpper = -300f;
    public float zoomBoundLower = -53f;

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
        float z = cam.transform.position.z;

        if (z < zoomBoundUpper)
        {
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, zoomBoundUpper);
            return;
        }
        if (z > zoomBoundLower)
        {
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, zoomBoundLower);
            return;
        }
        if (Input.GetKey(KeyCode.Equals))
        {
            cam.transform.position += cam.transform.forward * 1.0f * zoomSpeed;
        }
        else if (Input.GetKey(KeyCode.Minus))
        {
            cam.transform.position += cam.transform.forward * -1.0f * zoomSpeed;
        }

    }

    public void FocusOnPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
