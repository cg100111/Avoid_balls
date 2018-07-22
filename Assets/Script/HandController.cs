using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class HandController : MonoBehaviour {

    public PlayerController player_controller;
    public ThirdPersonViewController third_camera;
    public HandExtend hand_extend;
    public GameObject player_hand;

    private SteamVR_TrackedObject trackedObj;
    

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Set_extended_hand();
            hand_extend.Extend(2f);
        }
        if ((controller.GetHairTriggerDown() || Input.GetKeyDown(KeyCode.Space)) && hand_extend.is_extend)
        {
            StopCoroutine("Extend_to_front");
            player_controller.Move();
        }
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            third_camera.Change_Camera_angle(controller.GetAxis());
        }
        InputTracking.GetLocalPosition(VRNode.LeftHand);
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "body")
        {
            hand_extend.Record_first_pos();
        }
        else if (col.gameObject.tag == "wall")
        {
            hand_extend.Record_second_pos();
            Set_extended_hand();
        }
    }

    public void Recover_hand()
    {
        hand_extend.Initialize();
    }

    public int Get_which_hand()
    {
        return (int)trackedObj.index;
    }

    private void Set_extended_hand()
    {
        //注意
        player_controller.handIsExtended = player_hand;
    }

    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
}
