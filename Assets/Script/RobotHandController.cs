using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHandController : MonoBehaviour {

    public GameObject player_hand;
    public GameObject robot_hand;

    private const int max_time = 10;

    private Vector3 arm_org_position;
    private Vector3 arm_org_scale;
    private Vector3 hand_org_position;

    // Use this for initialization
    void Start()
    {
        arm_org_position = transform.localPosition;
        arm_org_scale = transform.localScale;
        hand_org_position = robot_hand.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Set_position()
    {
        transform.position = player_hand.transform.position;
        transform.rotation = player_hand.transform.rotation;
    }

    public void Extend(float velocity)
    {
        StartCoroutine(Extend_to_front(velocity));
    }

    private IEnumerator Extend_to_front(float speed)
    {
        //手を伸ばす
        for (int time = 0; time < max_time; time++)
        {
            transform.localScale += Vector3.right / 10f * speed;
            Revise_arm_position();
            Revise_hand_position();
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.2f);

        //手を引く
        for (int time = 0; time < max_time; time++)
        {
            transform.localScale -= Vector3.right / 10f * speed;
            Revise_arm_position();
            Revise_hand_position();
            yield return new WaitForSeconds(0.05f);
        }

        Reset_transform();
    }

    private void Revise_arm_position()
    {
        
        //Debug.Log(GetComponent<Renderer>().bounds.size);
    }

    private void Revise_hand_position()
    {
        
    }

    private void Reset_transform()
    {
        transform.localPosition = arm_org_position;
        transform.localScale = arm_org_scale;
        robot_hand.transform.localPosition = hand_org_position;
    }
}
