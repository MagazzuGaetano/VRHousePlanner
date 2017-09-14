using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public float rotSpeed = 20;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform plane;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 2 && PlanGraphics.currentMode != PlanGraphics.Modes._3D) return;

        if (Input.GetMouseButton(1))
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            plane.transform.Rotate(Vector3.up, rotX);
            transform.Rotate(Vector3.right, rotY);
        }

        if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Rotate();
        }

    }

    void Rotate()
    {
        if (Input.touchCount < 2 && PlanGraphics.currentMode != PlanGraphics.Modes._3D) return;

        if (Input.touchCount == 2 && PlanGraphics.currentMode == PlanGraphics.Modes._3D)
        {

            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            //limit x [-40,50]

            plane.transform.Rotate(Vector3.up, rotX);
            transform.Rotate(Vector3.right, rotY);

            /*yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            plane.eulerAngles = new Vector3(plane.position.y, pitch, 0.0f);
            transform.eulerAngles = new Vector3(pitch, 0f, 0.0f);

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);*/
        }
    }
    


}
