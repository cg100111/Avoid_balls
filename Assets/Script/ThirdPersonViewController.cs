using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonViewController : MonoBehaviour {

    public Transform LookAt;
    public Transform camTransform;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    //private Camera cam;
    private float distance = 1.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;

	private void Start()
    {
        camTransform = transform;
        //cam = Camera.main;
    }

    private void Update()
    {
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        //Debug.Log("position : " + transform.position);
        Test();
    }

    public void Change_Camera_angle(Vector2 angle)
    {
        if (angle.x <= -0.5f){
            currentX -= 2;
            Debug.Log("x-10");
        }
        else if (angle.x >= 0.5f)
        {
            currentX += 2;
            Debug.Log("x+10");
        }   
        if (angle.y <= -0.5f){
            currentY -= 2;
            Debug.Log("y-10");
        }
        else if (angle.y >= 0.5f)
        {
            currentY += 2;
            Debug.Log("y+10");
        }
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.A))
            currentX += 10;
        else if (Input.GetKeyDown(KeyCode.D))
            currentX -= 10;
        if (Input.GetKeyDown(KeyCode.W))
            currentY += 10;
        else if (Input.GetKeyDown(KeyCode.S))
            currentY -= 10;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = LookAt.position + rotation * dir;
        camTransform.LookAt(LookAt.position);
        //transform.position = Vector3.zero;
    }
}
