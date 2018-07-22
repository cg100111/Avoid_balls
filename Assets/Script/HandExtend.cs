using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandExtend : MonoBehaviour {

    public GameObject arm;
    public GameObject hand;

    private const int max_time = 10;

    private Vector3 arm_org_pos;
    private Vector3 arm_org_scale;
    private Vector3 hand_org_pos;
    private Vector3 start_pos;
    private Vector3 end_pos;
    private float time;
    private bool start_count = false;
    private bool is_extended = false;

    // Use this for initialization
    void Start () {
        Initialize_parameter();
        Record_beginning_value();
    }
	
	// Update is called once per frame
	void Update () {
        if (start_count)
            time += Time.deltaTime;
    }

    public void Initialize()
    {
        Initialize_parameter();
        Initialize_arm_and_hand();
    }

    public void Record_first_pos()
    {
        start_pos = transform.position;
        time = 0;
        start_count = true;
    }

    public void Record_second_pos()
    {
        if (start_count)
        {
            end_pos = transform.position;
            start_count = false;
            Calulation_velocity(time);
        }
    }

    //前に手を伸ばす
    private IEnumerator Extend_to_front(float speed)
    {
        Quaternion original_rotation = Quaternion.identity;
        //手を伸ばす
        for (int time = 0; time < max_time; time++)
        {
            //Extend1(speed, original_rotation);
            Extend2(speed);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.2f);

        //手を引く
        for (int time = 0; time < max_time; time++)
        {
            //Restore1(speed, original_rotation);
            Restore2(speed);
            yield return new WaitForSeconds(0.05f);
        }

        Initialize();
    }

    private void Record_beginning_value()
    {
        arm_org_pos = arm.transform.localPosition;
        arm_org_scale = arm.transform.localScale;
        hand_org_pos = hand.transform.localPosition;
    }

    private void Initialize_parameter()
    {
        time = 0;
        is_extended = false;
    }

    private void Initialize_arm_and_hand()
    {
        arm.transform.localScale = arm_org_scale;
        arm.transform.localPosition = arm_org_pos;
        hand.transform.localPosition = hand_org_pos;
    }

    private void Calulation_velocity(float time)
    {
        float velocity = Vector3.Distance(start_pos, end_pos) / time;
        Debug.Log("speed : " + velocity);
        Extend(velocity);
    }

    public void Extend(float velocity)
    {
        is_extended = true;
        StartCoroutine(Extend_to_front(velocity));
    }

    private void Extend1(float speed, Quaternion original_rotation)
    {
        original_rotation = arm.transform.parent.transform.rotation;
        Set_target_rotation_to_0();
        arm.transform.localScale += Vector3.right / 10f * speed;
        Reset_arm_local_position();
        Reset_hand_position();
        Restore_original_rotation(original_rotation);
    }

    private void Restore1(float speed, Quaternion original_rotation)
    {
        original_rotation = arm.transform.parent.transform.rotation;
        Set_target_rotation_to_0();
        arm.transform.localScale -= Vector3.right / 10f * speed;
        Reset_arm_local_position();
        Reset_hand_position();
        Restore_original_rotation(original_rotation);
    }
    
    private void Extend2(float speed)
    {
        arm.transform.localPosition -= Vector3.right / 2000f * speed;
    }

    private void Restore2(float speed)
    {
        arm.transform.localPosition += Vector3.right / 2000f * speed;
    }

    private void Set_target_rotation_to_0()
    {
        arm.transform.parent.transform.rotation = Quaternion.identity;
    }

    private void Reset_arm_local_position()
    {
        Vector3 arm_length = new Vector3(arm.GetComponent<Renderer>().bounds.size.x, 0f, 0f);
        arm.transform.localPosition = arm_org_pos - arm_length / 2;
    }

    private void Reset_hand_position()
    {
        float arm_length = arm.GetComponent<Renderer>().bounds.size.x;
        float hand_length = hand.GetComponent<Renderer>().bounds.size.x;
        float arm_pos = arm.transform.localPosition.x;
        hand.transform.localPosition = new Vector3(arm_pos - arm_length / 2 - hand_length / 2, hand_org_pos.y, hand_org_pos.z);
    }

    private void Restore_original_rotation(Quaternion original_rotation)
    {
        arm.transform.parent.transform.rotation = original_rotation;
    }

    public bool is_extend
    {
        get
        {
            return is_extended;
        }
    }
}
