using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

    public GameObject head;
    public PlayerController player_controller;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Set_position(head.transform.position);
        Set_rotation();
    }

    private void Set_position(Vector3 pos)
    {
        if (player_controller.Falldown)
            transform.position = new Vector3(pos.x, pos.y - 0.6f, pos.z);
        else
            transform.position = new Vector3(pos.x, 0f, pos.z);
    }

    private void Set_rotation()
    {
        transform.rotation = Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0);
    }
}
