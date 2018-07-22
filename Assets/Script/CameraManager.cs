using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Camera first_person_camera;
    public Camera third_person_camera;

    public void Change_to_third_person_camera()
    {
        first_person_camera.enabled = false;
        third_person_camera.enabled = true;
    }

    public void Change_to_first_person_camera()
    {
        first_person_camera.enabled = true;
        third_person_camera.enabled = false;
    }
}
